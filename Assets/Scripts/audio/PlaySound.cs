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
            aM = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        }

        public void sfx(string name)
        { 

            aM.Play(name);
        }
    }
}
