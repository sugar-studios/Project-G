using ProjectG.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace ProjectG.Audio
{
    public class PlaySound : MonoBehaviour
    {
        AudioManager aM;
        // Start is called before the first frame update
        void Start()
        {
            try
            {
            aM = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
            }
            catch { aM = null; }
        }

        public void sfx(string name)
        {
            if (aM != null)
            { 
                aM.Play(name);
            }
            
        }
    }
}
