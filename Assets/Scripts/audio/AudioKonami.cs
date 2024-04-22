using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectG.Audio
{
    using ProjectG.Settings;
    using UnityEngine;

    public class AudioKonami : MonoBehaviour
    {
        private readonly KeyCode[] konamiCode = {
        KeyCode.UpArrow, KeyCode.UpArrow,
        KeyCode.DownArrow, KeyCode.DownArrow,
        KeyCode.LeftArrow, KeyCode.RightArrow,
        KeyCode.LeftArrow, KeyCode.RightArrow,
    };

        public ResolutionSettings rS;
        public GameObject codeText;
        private int currentIndex = 0;

        public bool codeActive = false;

        private int resIndex = 0;

        private int[][] resolutions = {
            new int[] {1920, 1080},
            new int[] {640, 360},
            new int[] {480, 270}
        };

        void Update()
        {
            if (codeActive && currentIndex < konamiCode.Length)
            {
                if (Input.GetKeyDown(konamiCode[currentIndex]))
                {
                    currentIndex++;
                    if (currentIndex == konamiCode.Length)
                    {
                        ActivateCheat();
                        currentIndex = 0;
                    }
                    else
                    {
                        Debug.Log(konamiCode[currentIndex]);
                    }
                }
                else if (Input.anyKeyDown)
                {
                    currentIndex = 0;
                }
            }
        }


        void ActivateCheat()
        {
            codeText.SetActive(true);
            StartCoroutine(ExampleCoroutine());
            Debug.Log("Konami Code activated!");
            if (resIndex >= 0 && resIndex < resolutions.Length)
            {
                int[] resolution = resolutions[resIndex];
                rS.Res(resolution[0], resolution[1]);

                // Increment resIndex and reset it if it exceeds the length of resolutions array
                resIndex++;
                if (resIndex >= resolutions.Length)
                {
                    resIndex = 0;
                }
            }
            else
            {
                Debug.LogWarning("Invalid resolution index!");
            }
        }

        IEnumerator ExampleCoroutine()
        {
            yield return new WaitForSeconds(2);
            codeText.SetActive(false);
        }

    }

}
