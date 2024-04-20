using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectG.Results
{
    public class ResultsAnimations : MonoBehaviour
    {
        private ResultsManager rManager;

        private void Start()
        {
            rManager = GameObject.FindWithTag("ResultsManager").GetComponent<ResultsManager>();
        }

        public void StartStatAnim()
        {
            StartCoroutine(WaitThenActivate(rManager.burgerMenu, 0));
        }
        public void StartBannerAnim()
        {
            StartCoroutine(WaitThenActivate(rManager.burgerBanner, 0));
        }
        public void StartButttonAnim()
        {
            StartCoroutine(WaitThenActivate(rManager.burgerButtons, 0.5f));
        }

        IEnumerator WaitThenActivate(GameObject obj, float time = 1f)
        {
            yield return new WaitForSeconds(time);
            obj.SetActive(true);
        }
    }
}
