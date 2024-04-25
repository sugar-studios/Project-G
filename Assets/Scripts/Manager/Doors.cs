using ProjectG.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectG.Manger
{
    public class Doors : MonoBehaviour
    {
        [Tooltip("The spawn point where the player should appear when using this door.")]
        public Transform spawnPoint;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                // Teleport the player to the linked spawn point
                other.gameObject.SetActive(false);
                other.transform.position = spawnPoint.position;
                other.gameObject.SetActive(true);
                // Optionally, you might want to adjust the player's rotation or other properties
                Debug.Log("Player teleported to: " + spawnPoint.position);
                try
                {
                    this.GetComponent<PlaySound>().sfx("Bird Alarm");
      
                }
                catch { }
            }
        }
    }

}

