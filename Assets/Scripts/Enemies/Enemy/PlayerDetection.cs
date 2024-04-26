using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectG.Debugging;
using UnityEditor;

namespace ProjectG.Enemies.Enemy
{
    public class PlayerDetection : MonoBehaviour
    {
        public bool seePlayer;
        public bool inView;
        public bool inHearing;
        public bool overrideHeardingOff;
        public bool overrideSeePlayer;
        public bool overrideInRange;
        public bool inRange;

        [Tooltip("x value is the closer value, y is the futher value")]
        public Vector2 enemyPlayerRange;
        private float angle;
        private Vector3 enemyToPlayer;


        public Transform player;

        private void Start()
        {
            player = GameObject.Find("PlayerController").transform;
        }

        public void OverrideHearingOff()
        { 
            inHearing = false;
        }

        private void Update()
        {
            if (Vector3.Distance(player.position, transform.position) < enemyPlayerRange.y && Vector3.Distance(player.position, transform.position) > enemyPlayerRange.x || overrideInRange)
            {
                inRange = true;
            }
            else
            {
                inRange = false;
            }

            enemyToPlayer = player.position - transform.position;

            /* 
             * 
             * LEGACY HEARING
             * 
            if (overrideHeardingOff)
            {
                //inHearing = false;
            }
            else if (Vector3.Distance(transform.position, player.position) < PlayerStatesTester.PlayerNoiseRadius)
            {
                inHearing = true;
            }
            else
            {
                inHearing = false;
            }
            */

            angle = Vector3.Angle(transform.forward, enemyToPlayer);

            if (angle < 70f)
            {
                inView = true;
            }
            else
            {
                inView = false;
            }

            if (inView && inHearing || overrideSeePlayer)
            {
                seePlayer = true;
            }
            else
            {
                seePlayer = false;
            }
        }
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            enemyToPlayer = player.position - transform.position;
            angle = Vector3.Angle(transform.forward, enemyToPlayer);


            Ray line1 = new Ray(transform.position, transform.forward);
            Ray line2 = new Ray(transform.position, enemyToPlayer);

            Gizmos.DrawRay(line1);
            Gizmos.DrawRay(line2);

            Handles.Label(transform.position, $"angle: {angle}");

        }
#endif
    }
}
