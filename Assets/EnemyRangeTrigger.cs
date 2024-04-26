using UnityEngine;

namespace ProjectG.Enemies.Enemy
{
    public class EnemyRangeTrigger : MonoBehaviour
    {
        public PlayerDetection playerDetection; // Reference to PlayerDetection

        private void Start()
        {
            playerDetection.inRange = false;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Kill player");
                playerDetection.inRange = true; // Set inRange to true when the player enters the trigger
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Dont player");
                playerDetection.inRange = false; // Set inRange to false when the player exits the trigger  PlayerRangeDetect
                playerDetection.overrideInRange();
            }
        }
    }
}
