using ProjectG.Enemies.Enemy;
using System.Collections;
using UnityEngine;

namespace ProjectG.Enemies.Enemy
{
    public class EnemySightTrigger : MonoBehaviour
    {
        public PlayerDetection playerDetection;
        public Transform enemyOrigin; // Assign this in the Inspector
        public Transform playerOrigin; // Assign this in the Inspector
        public LayerMask excludedLayers; // Assign which layers to exclude in the Inspector

        private void Start()
        {
            playerOrigin = GameObject.FindGameObjectWithTag("PlayerOrgin").transform; // Ensure the tag is spelled correctly
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                playerDetection.playerInSightTrigger = true;
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                playerDetection.playerInSightTrigger = true;
                CheckLineOfSight();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                playerDetection.playerInSightTrigger = false;
                playerDetection.overrideSeePlayer = false;
            }
        }

        private void CheckLineOfSight()
        {
            RaycastHit hit;
            Vector3 directionToPlayer = playerOrigin.position - enemyOrigin.position;

            // Draw a red ray from the enemy to the player
            Debug.DrawRay(enemyOrigin.position, directionToPlayer, Color.red);

            // Use the inverse of excludedLayers to calculate which layers to include in the raycast
            if (Physics.Raycast(enemyOrigin.position, directionToPlayer, out hit, Mathf.Infinity, ~excludedLayers))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    playerDetection.overrideSeePlayer = true;
                }
                else
                {
                    Debug.Log(hit.collider.gameObject.name);
                    playerDetection.overrideSeePlayer = false;
                }
            }
        }
    }
}
