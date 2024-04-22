using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ProjectG.UI;
using UnityEngine.SceneManagement;
using ProjectG.Player;

namespace ProjectG.Manger
{
    public class GameManager : MonoBehaviour
    {
        public GameObject player;
        public GameObject map;
        public GameObject biestro13;
        public GameObject adminOffice;
        public GameObject GameUI;
        public Transform[] mainSpawnPoints;
        public GameObject[] exitPoints;
        public GameObject Transition;
        public GameObject gameOver;
        public GameObject loadScreen;
        public UnityEngine.UI.Slider loadingBar;


        public MeterGauge birdMeter;

        public int score = 0;

        public bool isInBiestro = true;

        public GameUIManager UIManager;

        private Transform biestroSpawn;

        private GameObject exitBiestroTrigger;
        private GameObject mealReceiveTrigger;
        private GameObject mealDeliverTrigger;

        private TextMeshProUGUI console;

        private bool isReceiveTriggerActive = true;

        void Start()
        {
            UnityEngine.Cursor.visible = false;

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

            UIManager.UpdateScore(score);

            isInBiestro = true;
            unpauseBirds();
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
                HandleMealReceived();
            }
            else if (triggerName == mealDeliverTrigger.name && !isReceiveTriggerActive)
            {
                HandleMealDelivered();
            }
            else if (triggerName == exitBiestroTrigger.name && !isReceiveTriggerActive)
            {
                HandleExitBiestro();
            }
            else if (triggerName.StartsWith("returnPoint"))
            {
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

        void HandleMealDelivered()
        {
            UIManager.ClearAndStopTyping(console);
            UIManager.TypeText(console, "Delivered Meal! Go to exit point!", 2.5f);

            turnOffAllExits();
            exitPoints[Random.Range(0, exitPoints.Length)].SetActive(true);

            score++;
            UIManager.UpdateScore(score);

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

        public void GameOver()
        {
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
            StartCoroutine(LoadAsync(scene));
        }

    }
}

