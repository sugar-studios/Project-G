using ProjectG.Debugging;
using ProjectG.Enemies;
using ProjectG.Enemies.Handler;
using ProjectG.Player;
using ProjectG.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectG.Items
{
    public class itemUsageMethods : MonoBehaviour
    {

        int startValue;


        public void useTest()
        {
            Debug.Log("use item");
        }

        public void panUse()
        {
            Debug.Log("used pan");
            PlayerStatesTester.PlayerNoiseRadius += 100;

            FindFirstObjectByType<SpawnHandler>().checkNearby();

            GetComponent<InventoryHandler>().BirdAttack.paused = true;

            StartCoroutine(turnOffBirds(8));

            //PlayerStatesTester.PlayerNoiseRadius -= 100;

        }

        public void regenHealth()
        {
            PlayerMovement move = GetComponent<PlayerMovement>();

            move.playerHealth = Mathf.Clamp(move.playerHealth + 20, -1, 100);

        }

        IEnumerator turnOffBirds(float duration)
        {
            // do something before
            Debug.Log("Before");


            // waits here
            yield return new WaitForSeconds(duration);

           GetComponent<InventoryHandler>().BirdAttack.paused = false;

            // do something after
            Debug.Log("After");

        }
    }
}
