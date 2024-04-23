using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectG.UI
{
    public class TitleCreditsCheckObj : MonoBehaviour
    {
        private void Start()
        {
            if (!TitleCreditsCheck.seenCredits)
            {
                gameObject.SetActive(true);
                TitleCreditsCheck.seenCredits = true;
            }
            else
            {
                gameObject.SetActive(false);
            }

        }
    }
}
