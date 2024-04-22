using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectG.Settings
{
    public class ResolutionSettings : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
      
            Screen.SetResolution(480, 270, true);
            int screenWidth = Screen.width;
            int screenHeight = Screen.height;

            Debug.Log("Screen Resolution: " + screenWidth + "x" + screenHeight);
        }

        public void Res(int x, int y)
        {
            Screen.SetResolution(x, y, true);
        }
    }
}
