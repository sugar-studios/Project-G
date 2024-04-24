using ProjectG.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectG.Results
{
    public class resultsData : MonoBehaviour
    {
        public float health = 0f;
        public float mealsDelivered = 0f;
        public float tip = 0f;

        public static resultsData instance;


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
