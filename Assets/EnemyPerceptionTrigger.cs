using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectG.Enemies.Enemy;

namespace ProjectG.Player
{
    public class EnemyPerceptionTrigger : MonoBehaviour
    {
        public PlayerDetection playerDetection; 

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("EnemyEars"))
            {
                playerDetection = other.gameObject.transform.GetComponentInParent<PlayerDetection>();
                playerDetection.inHearing = true; // Set inRange to true when the player enters the trigger
                playerDetection = null;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("EnemyEars"))
            {
                playerDetection = other.gameObject.transform.GetComponentInParent<PlayerDetection>();
                playerDetection = null;
            }
        }
    }
}
