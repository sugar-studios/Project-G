using ProjectG.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectG.Audio
{
    public class PlaylistSetting : MonoBehaviour
    {
        public static PlaylistSetting instance;


        public string perferredSong = "";
        [Range(0f, 1f)]
        public float volume = 1f;

        void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
    
        }
    }
}
