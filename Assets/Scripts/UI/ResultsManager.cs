using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Unity.VisualScripting;
using ProjectG.UI;
using ProjectG.Manager;
using ProjectG.Audio;
using TMPro;
using UnityEngine.Events;

namespace ProjectG.Results
{
    public class ResultsManager : MonoBehaviour
    {

        public GameObject burgerBanner;
        public GameObject burgerMenu;
        public GameObject burgerTitle;
        public GameObject resultsBackground;
        public GameObject burgerButtons;

        public TMP_InputField userName;

        public resultsData resultsData;
        bool menu = false;

        public GameObject resultsScreen;
        public GameObject loadingScreen;

        private AudioManager audioManager;
        private Leaderboard lM;

        public GameObject[] Screens;

        public Slider progressBar;

        public int rotSpeed = 30;
        public int score = 0;
        [SerializeField]
        bool scored;

        // Start is called before the first frame update
        void Start()
        {
            InitializeUI();
            CalculateRank();
            scored = false;
        }


        void InitializeUI()
        {
            try
            {
                resultsData = GameObject.FindGameObjectWithTag("ResultData").GetComponent<resultsData>();
            }
            catch
            {
                resultsData = null;
                Debug.Log("ERROR: missing resultsData");
            }
            try
            {
                audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
            }
            catch {
                Debug.Log("no audio manager");
            }
            try
            {
                burgerTitle.SetActive(true);
                burgerBanner.SetActive(false);
                burgerMenu.SetActive(false);
                burgerButtons.SetActive(false);
                menu = true;
            }
            catch
            {
                Debug.Log("ERROR: missing basically everything!");
                menu = false;
            }
            try
            {
                resultsBackground = GameObject.FindGameObjectWithTag("ResultsBackground");
            }
            catch
            {
                resultsBackground = null;
                Debug.Log("ERROR: missing bg");
            }

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None; // Unlock the cursor
        }

        void CalculateRank()
        {
            if (menu && resultsData != null)
            {
                SetMenuText();
                TextMeshProUGUI rank = burgerBanner.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

                int mealsDelivered = (int)resultsData.mealsDelivered;
                int totalTip = (int)resultsData.tip;
                int healthLeft = (int)resultsData.health;

                // Calculate a score based on meals delivered, total tip, and health left
                score = mealsDelivered * 10 + totalTip + healthLeft;

                // Determine rank based on the score
                string rankText;
                if (score >= 500)
                    rankText = "S++";
                else if (score >= 570)
                    rankText = "S+";
                else if (score >= 540)
                    rankText = "S";
                else if (score >= 510)
                    rankText = "S-";
                else if (score >= 380)
                    rankText = "A+";
                else if (score >= 350)
                    rankText = "A";
                else if (score >= 320)
                    rankText = "A-";
                else if (score >= 290)
                    rankText = "B+";
                else if (score >= 260)
                    rankText = "B";
                else if (score >= 230)
                    rankText = "B-";
                else if (score >= 200)
                    rankText = "C+";
                else if (score >= 170)
                    rankText = "C";
                else if (score >= 140)
                    rankText = "C-";
                else if (score >= 110)
                    rankText = "D+";
                else if (score >= 180)
                    rankText = "D";
                else if (score >= 90)
                    rankText = "D-";
                else if (score >= 60)
                    rankText = "F+";
                else if (score >= 30)
                    rankText = "F";
                else if (score >= 20)
                    rankText = "F-";
                else
                    rankText = "F--";

                // Set the rank text to the UI element
                rank.text = rankText;
                scored = true;
            }
        }

        void SetMenuText()
        {
            burgerMenu.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Meals Delivered: " + ((int)resultsData.mealsDelivered).ToString();
            burgerMenu.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Total Tip: " + ((int)resultsData.tip).ToString();
            burgerMenu.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Health Left: " + ((int)resultsData.health).ToString();
        }

        public void LoadLevel(string scene)
        {
            try
            {
                resultsScreen.SetActive(false);
            }
            catch {
                try
                {
                    for (int i = 0; i < Screens.Length; i++)
                    {
                        Screens[i].SetActive(false);
                    }
                }
                catch 
                {
                    Debug.LogWarning("This is not good");
                }
            }
            loadingScreen.SetActive(true);
            StartCoroutine(LoadAsync(scene));
        }

        IEnumerator LoadAsync(string sceneName)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);


            while (!operation.isDone)
            {
                float progress = Mathf.Clamp01(operation.progress / .9f);
                progressBar.value = progress;

                yield return null;
            }
        }

        public void ExitApplication()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; // This stops play mode in the editor
#else
            Application.Quit(); // This exits the application when not in editor
#endif
        }


        public void navigate(GameObject Screen)
        {
            for (int i = 0; i < Screens.Length; i++)
            {
                Screens[i].SetActive(false);
            }
            Screen.SetActive(true);
        }

        private void Update()
        {
            if (resultsBackground != null)
            {
                float rotationAmount = rotSpeed * Time.deltaTime;

                resultsBackground.transform.Rotate(Vector3.forward, rotationAmount);
            }
        }
    }
}


// public key: "40ae65e747285251ce9d29d8635c70a7504fdc74cd748879bbb34b5d5f49787d"
