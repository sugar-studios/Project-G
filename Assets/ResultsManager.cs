using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ProjectG
{
    public class ResultsManager : MonoBehaviour
    {

        public GameObject burgerBanner;
        public GameObject burgerMenu;
        public GameObject burgerTitle;
        public GameObject burgerButtons;

        public GameObject resultsScreen;
        public GameObject loadingScreen;

        public AudioClip[] sfx;

        public Slider progressBar;

        // Start is called before the first frame update
        void Start()
        {
            burgerTitle.SetActive(true);
            burgerBanner.SetActive(false);
            burgerMenu.SetActive(false);
            burgerButtons.SetActive(false);
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

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
