using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ProjectG.UI;
using UnityEngine.SceneManagement;
using ProjectG.Player;
using ProjectG.Audio;
using ProjectG.Results;

namespace ProjectG.Manger
{
    public class GameManager : MonoBehaviour
    {
        public GameObject player;
        public float mealsDel = 0f;
        public GameObject map;
        public GameObject biestro13;
        public GameObject adminOffice;
        public GameObject GameUI;
        public Transform[] mainSpawnPoints;
        public GameObject[] exitPoints;
        public GameObject Transition;
        public GameObject gameOver;
        public PlaySound sound;
        public bool DeleiveringMeal=false;
        public GameObject loadScreen;
        public UnityEngine.UI.Slider loadingBar;

        public float totalTime = 180f; // Total time in seconds (3 minutes)
        private float timeRemaining;
        public resultsData rD;
        public TMP_Text countdownText;

        public MeterGauge birdMeter;

        public float score = 0.00f;

        private bool loggedResults=false;

        public bool isInBiestro = true;

        public GameUIManager UIManager;

        private Transform biestroSpawn;

        private GameObject exitBiestroTrigger;
        private GameObject mealReceiveTrigger;
        private GameObject mealDeliverTrigger;


        public float value = 0.00f;
        private float decreaseAmount = 0.5f;
        private float decreaseInterval = 1f; // Decrease every 1 second

        private TextMeshProUGUI console;

        private bool isReceiveTriggerActive = true;
        bool menuOPen = false;

        void Start()
        {
            UnityEngine.Cursor.visible = false;
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;

            loggedResults = false;

            rD = GameObject.FindGameObjectWithTag("ResultData").GetComponent<resultsData>();

            gameOver.SetActive(false);

            exitBiestroTrigger = biestro13.transform.GetChild(1).gameObject;
            mealReceiveTrigger = biestro13.transform.GetChild(0).gameObject;
            mealDeliverTrigger = adminOffice.transform.GetChild(0).gameObject;

            biestroSpawn = biestro13.transform.GetChild(2);

            player.SetActive(false);
            player.transform.position = biestroSpawn.position;
            player.SetActive(true);

            mealDeliverTrigger.GetComponent<Collider>().enabled = false;
            exitBiestroTrigger.SetActive(false);

            console = GameUI.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
            UIManager.TypeText(console, "Administrion ordered their lunch! Go!", 2.5f);

            UIManager.UpdateTotalScore(score);
            UIManager.UpdateScore(value);

            isInBiestro = true;
            unpauseBirds();
            StartCountdown();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !menuOPen)
            { 
                UIManager.OpenPauseMenu();
                menuOPen = true;
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && menuOPen)
            {
                UIManager.ClosePauseMenu();
                menuOPen= false;
            }
            if (player.GetComponent<PlayerMovement>().playerHealth <= 0)
            {
                GameOver();
            }
        }

        void unpauseBirds()
        {
            birdMeter.paused = isInBiestro;
            birdMeter.value = isInBiestro ? -100 : 0;
        }

        public void HandleTriggerEnter(string triggerName)
        {
            if (triggerName == mealReceiveTrigger.name && isReceiveTriggerActive)
            {
                sound.sfx("Get Food");
                HandleMealReceived();
                value = 51.50f;
                DeleiveringMeal = true;
                InvokeRepeating("DecreaseValue", 0f, decreaseInterval);
            }
            else if (triggerName == mealDeliverTrigger.name && !isReceiveTriggerActive)
            {
                sound.sfx("Give Food");
                DeleiveringMeal = false;
                CancelInvoke("DecreaseValue");
                score += value;
                value = 0f;
                mealsDel++;
                UIManager.UpdateScore(value);
                UIManager.UpdateTotalScore(score);
                CancelInvoke("DecreaseValue");
                HandleMealDelivered();
            }
            else if (triggerName == exitBiestroTrigger.name && !isReceiveTriggerActive)
            {
                sound.sfx("Exit B");
                HandleExitBiestro();
            }
            else if (triggerName.StartsWith("returnPoint"))
            {
                sound.sfx("Exit EVIT");
                SendPlayerToBiestro();
                turnOffAllExits();
            }
            else if (triggerName == "biestroEntrance") // Assuming this is the trigger for entering Biestro
            {
                ResetBirdTimer();
            }
        }

        void HandleMealReceived()
        {
            UIManager.ClearAndStopTyping(console);
            UIManager.TypeText(console, "Recieved Meal!", 2.5f);
            isReceiveTriggerActive = false;

            mealReceiveTrigger.GetComponent<Collider>().enabled = false;
            mealDeliverTrigger.GetComponent<Collider>().enabled = true;
            mealDeliverTrigger.GetComponent<MeshRenderer>().enabled = true;
            mealReceiveTrigger.GetComponent<MeshRenderer>().enabled = false;
            mealReceiveTrigger.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
            exitBiestroTrigger.SetActive(true);
        }

        public void StartCountdown()
        {
            timeRemaining = totalTime;
            InvokeRepeating("UpdateCountdown", 0f, 1f); 
        }

        void UpdateCountdown()
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= 1f;
                UpdateCountdownText();
            }
            else
            {
                timeRemaining = 0;
                Debug.Log("Time's up!");
                GameOver();
                CancelInvoke("UpdateCountdown"); 
            }
        }

        void UpdateCountdownText()
        {
            int minutes = Mathf.FloorToInt(timeRemaining / 60f);
            int seconds = Mathf.FloorToInt(timeRemaining % 60f);

            countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }

        void HandleMealDelivered()
        {
            UIManager.ClearAndStopTyping(console);
            UIManager.TypeText(console, "Delivered Meal! Go to exit point!", 2.5f);

            turnOffAllExits();
            exitPoints[Random.Range(0, exitPoints.Length)].SetActive(true);

            isReceiveTriggerActive = true;

            mealReceiveTrigger.GetComponent<Collider>().enabled = true;
            mealReceiveTrigger.GetComponent<MeshRenderer>().enabled = true;
            mealReceiveTrigger.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
            mealDeliverTrigger.GetComponent<Collider>().enabled = false;
            mealDeliverTrigger.GetComponent<Renderer>().enabled = false;
        }

        void HandleExitBiestro()
        {
            Transition.SetActive(true);

            Transform selectedPoint = mainSpawnPoints[Random.Range(0, mainSpawnPoints.Length)];
            Vector3 spawnPosition = selectedPoint.position;

            player.SetActive(false);
            player.transform.position = spawnPosition;

            exitBiestroTrigger.SetActive(false);
            isInBiestro = false;

            WaitForVariableToBeTrue(Transition.GetComponent<DeActivateTrans>().sceneLoad);
            player.SetActive(true);

            unpauseBirds();
        }

        public void SendPlayerToBiestro()
        {
            Transition.SetActive(true);
            player.SetActive(false);
            player.transform.position = biestroSpawn.position;
            player.transform.rotation = Quaternion.identity;
            isInBiestro = true;
            unpauseBirds();
            WaitForVariableToBeTrue(Transition.GetComponent<DeActivateTrans>().sceneLoad);
            player.SetActive(true);
            player.GetComponent<PlayerMovement>().speed = 10;
            player.GetComponent<PlayerMovement>().sprintSpeed = 25;
            player.GetComponent<PlayerMovement>().currentStamina = player.GetComponent<PlayerMovement>().maxStamina;
        }

        void turnOffAllExits()
        {
            foreach (var exitPoint in exitPoints)
            {
                exitPoint.SetActive(false);
            }
        }

        IEnumerator WaitForVariableToBeTrue(bool waitForThisVariable)
        {
            while (!waitForThisVariable)
            {
                yield return null;
            }

        }

        public void UpdateStamina(float num)
        {
            UnityEngine.UI.Slider slider = GameUI.transform.GetChild(3).GetChild(0).GetComponent<UnityEngine.UI.Slider>();
            slider.value = num;

            UnityEngine.UI.Image sliderFill = slider.transform.GetChild(1).GetChild(0).GetComponent<UnityEngine.UI.Image>();

            Color green = new Color(15f / 255, 108f / 255, 2f / 255);
            Color yellow = Color.yellow;
            Color red = new Color(193f / 255, 22f / 255, 0f / 255);

            float minValue = slider.minValue;
            float maxValue = slider.maxValue;
            float midValue = (maxValue + minValue) / 2;

            if (num >= midValue)
            {
                float t = (num - midValue) / (maxValue - midValue);
                sliderFill.color = Color.Lerp(yellow, green, t);
            }
            else
            {
                float t = (num - minValue) / (midValue - minValue);
                sliderFill.color = Color.Lerp(red, yellow, t);
            }
        }

        public void SetMaxStamina(float num, float num2)
        {
            UnityEngine.UI.Slider slider = GameUI.transform.GetChild(3).GetChild(0).GetComponent<UnityEngine.UI.Slider>();
            slider.maxValue = num;
            slider.minValue = num2;
        }

        void ResetBirdTimer()
        {
            birdMeter.value = -100;
        }

        IEnumerator LoadAsync(string sceneName)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

            while (!operation.isDone)
            {
                float progress = Mathf.Clamp01(operation.progress / .9f);
                loadingBar.value = progress;

                yield return null;
            }
        }

        void LogData()
        {
            if (!loggedResults)
            {
                if (player.GetComponent<PlayerMovement>().playerHealth <= 0)
                {
                    rD.health = 0f;
                }
                else if (player.GetComponent<PlayerMovement>().playerHealth > 0)
                {
                    rD.health = player.GetComponent<PlayerMovement>().playerHealth;
                }
                else
                {
                    rD.health = 100f;
                }

                rD.tip = score;
                rD.mealsDelivered = mealsDel;

                loggedResults = true;
            }
        }


        public void GameOver(bool hitByCar = false)
        {
            if (hitByCar)
            {
                rD.health = 0f;
                rD.tip = score;
                rD.mealsDelivered = mealsDel;
                loggedResults = true;
            }
            else
            { 
                LogData();
            }
            player.GetComponent<PlayerMovement>().enabled = false;
            player.GetComponent<CharacterController>().enabled = false;

            StartCoroutine(GameOverCoroutine());
        }

        private IEnumerator GameOverCoroutine()
        {
            player.GetComponent<PlayerMovement>().enabled = false;
            player.transform.GetChild(0).GetChild(1).GetComponent<Animator>().enabled = false;
            player.GetComponent<PlayerGraphics>().enabled = false;
            gameOver.SetActive(true);

            yield return new WaitForSeconds(4f); // Wait for 2 seconds

            StartLoadScene(); // Call StartLoadScene after waiting
        }

        public void StartLoadScene(string scene = "Results")
        {
            gameOver.SetActive(false);
            loadScreen.SetActive(true);
            SceneManager.LoadScene(scene);
        }

        void DecreaseValue()
        {
            // Decrease the value
            value -= decreaseAmount;
            UIManager.UpdateScore(value);

            // Check if the value has reached or gone below zero
            if (value <= 1f)
            {
                // If it has, stop the decreasing process
                value = 1f;
                CancelInvoke("DecreaseValue");
            }
        }

    }
}

