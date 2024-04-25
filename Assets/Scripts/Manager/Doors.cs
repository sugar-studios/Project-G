using ProjectG.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectG.Manager
{
    public class Doors : MonoBehaviour
    {
        [Tooltip("The spawn point where the player should appear when using this door.")]
        public Transform spawnPoint;

        public DoorEntryCheck DEC;
        public GameObject DECobj;
        private bool isPlayerInTrigger = false;
        private bool tempDisable = false;

        private void OnTriggerEnter(Collider other)
        {
            DECobj.SetActive(true);
            if (other.CompareTag("Player") && !tempDisable)
            {
                isPlayerInTrigger = true;
                StartCoroutine(HandleDoorEntry(other));
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player") && tempDisable)
            {
                isPlayerInTrigger = false;
                DECobj.SetActive(false);
                DEC.Enter = '0';
                StartCoroutine(HandleDoorDisable());
            }
            else
            {
                isPlayerInTrigger = false;
                DECobj.SetActive(false);
                DEC.Enter = '0';
            }
        }

        private IEnumerator HandleDoorDisable()
        {
            yield return new WaitForSeconds(1);
            tempDisable = false;
        }

        private IEnumerator HandleDoorEntry(Collider player)
        {
            while (isPlayerInTrigger)
            {
                if (DEC.Enter == '1')
                {
                    TeleportPlayer(player);
                    DECobj.SetActive(false);
                    DEC.Enter = '0';
                    yield break;
                }
                else if (DEC.Enter == '2')
                {
                    DEC.Enter = '0';
                    DECobj.SetActive(false);
                    tempDisable = true;
                }
                yield return null;
            }
        }

        private void TeleportPlayer(Collider player)
        {
            player.gameObject.SetActive(false);
            player.transform.position = spawnPoint.position;
            player.gameObject.SetActive(true);
            Debug.Log("Player teleported to: " + spawnPoint.position);
        }
    }
}
