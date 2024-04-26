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
        public bool inRange;
        public bool playerInSightTrigger; 

        [Tooltip("x value is the closer value, y is the further value")]
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
        public void overrideInRange()
        {
            inRange = false;
        }

        private void Update()
        {
            // Removed inRange control from here to allow trigger-based control
            enemyToPlayer = player.position - transform.position;
            angle = Vector3.Angle(transform.forward, enemyToPlayer);

            if (angle < 70f)
            {
                inView = true;
            }
            else
            {
                inView = false;
            }

            seePlayer = overrideSeePlayer;

            // Optionally update inRange based on both distance and override, only if overrideInRange is true
            /*if (overrideInRange)
            {
                inRange = Vector3.Distance(player.position, transform.position) < enemyPlayerRange.y && Vector3.Distance(player.position, transform.position) > enemyPlayerRange.x;
            }
            */
            //Debug.Log(inHearing);
        }



#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            /*
            enemyToPlayer = player.position - transform.position;
            angle = Vector3.Angle(transform.forward, enemyToPlayer);

            Ray line1 = new Ray(transform.position, transform.forward);
            Ray line2 = new Ray(transform.position, enemyToPlayer);

            Gizmos.DrawRay(line1);
            Gizmos.DrawRay(line2);

            Handles.Label(transform.position, $"angle: {angle}");
            */
        }
#endif
    }
}
