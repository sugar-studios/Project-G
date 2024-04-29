using ProjectG.Debugging;
using ProjectG.Enemies;
using ProjectG.Enemies.Enemy;
using ProjectG.Enemies.Handler;
using ProjectG.Manger;
using ProjectG.Player;
using ProjectG.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ProjectG.Items
{
    public class itemUsageMethods : MonoBehaviour
    {

        int startValue;
        bool ableToShoot = true;
        public LayerMask enemy;
        public Transform cam;
        public GameObject hitmarker;


        public void useTest()
        {
            Debug.Log("use item");
        }

        public void useWatch()
        { 
            GameManager gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
            gm.PauseCountdown();
        }

        public void usePhone()
        {
            GameManager gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
            gm.SendPlayerToBiestro();
        }

        public void useJump()
        {
            PlayerMovement move = GetComponent<PlayerMovement>();

            move.jumpHeight += 2.5f;
        }
        public void useStamin()
        {
            PlayerMovement move = GetComponent<PlayerMovement>();

            move.currentStamina += 25;
            if (move.currentStamina > move.maxStamina)
            {
                move.currentStamina = move.maxStamina;
            }
        }

        public void useBoost()
        {
            StartCoroutine(Boost(10));
        }

        private IEnumerator Boost(int time)
        {
            PlayerMovement move = GetComponent<PlayerMovement>();

            if (move == null) yield break; // Exit if no PlayerMovement component is found

            // Store original speeds
            float originalSpeed = move.speed;
            float originalSprintSpeed = move.sprintSpeed;

            // Increase speed
            move.speed *= 2.5f;
            move.sprintSpeed *= 2.5f;

            // Wait for 10 seconds
            yield return new WaitForSeconds(time);

            // Restore original speeds
            move.speed = originalSpeed;
            move.sprintSpeed = originalSprintSpeed;
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
            move.updateHealth();

        }

        public void UseNerfGun()
        {
            if (ableToShoot)
            {
                Debug.Log("nerf or nothing");

                RaycastHit hit;

                if (Physics.Raycast(cam.position, cam.forward, out hit, 70f, enemy))
                {
                    Debug.Log("hit enemy");
                    hitmarker.SetActive(true);
                    hit.transform.GetComponent<NavMeshAgent>().speed -= 3;
                    hit.transform.GetComponent<EnemyMovement>().stunned = true;
                    StartCoroutine(shootingCooldown(2, hit.transform.GetComponent<EnemyMovement>()));
                }
                else
                {
                    StartCoroutine(shootingCooldown(2, new EnemyMovement()));
                }

                ableToShoot = false;
            }
        }

        IEnumerator shootingCooldown(float coolDownTime, EnemyMovement enemy)
        {
            Debug.Log("cant shoot");

            yield return new WaitForSeconds(coolDownTime);

            ableToShoot = true;

            hitmarker.SetActive(false);

            enemy.stunned = false;

            try 
            { 
                enemy.GetComponent<NavMeshAgent>().speed += 3; 
            }
            catch(NullReferenceException e)
            {
                Debug.Log(e);
            }

            Debug.Log("can shoot again");
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
