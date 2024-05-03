using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

namespace ProjectG.Manager
{
    public class leadINit : MonoBehaviour
    {
        public Leaderboard lM;

        private void Start()
        {
            try
            {
                lM = GameObject.FindGameObjectWithTag("LeaderboardManager").GetComponent<Leaderboard>();
                lM.GetLeaderboard();
            }
            catch
            {
                Debug.Log("no leaderboard");
            }
        }
    }
}
