using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Unity.VisualScripting;

namespace ProjectG.Results
{
    public class ResultsManager : MonoBehaviour
    {

        public GameObject burgerBanner;
        public GameObject burgerMenu;
        public GameObject burgerTitle;
        public GameObject burgerButtons;

        public GameObject resultsScreen;
        public GameObject loadingScreen;

        public GameObject[] Screens;

        public AudioClip[] sfx;

        public Slider progressBar;

        // Start is called before the first frame update
        void Start()
        {
            try
            {
                burgerTitle.SetActive(true);
                burgerBanner.SetActive(false);
                burgerMenu.SetActive(false);
                burgerButtons.SetActive(false);
            }
            catch
            {
                Debug.Log("ERROR: missing basically everything!");
            }

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None; // Unlock the cursor
        }

        public void LoadLevel(string scene)
        { 
            resultsScreen.SetActive(false);
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


        public void navigate(GameObject Screen)
        { 
            for (int i = 0; i < Screens.Length; i++)
            {
                Screens[i].SetActive(false);
            }
            Screen.SetActive(true);
        }
    }
}
