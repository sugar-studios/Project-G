using ProjectG.Audio;
using ProjectG.Player;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

namespace ProjectG.Enemies.Enemy
{
    public class EnemyMovement : MonoBehaviour
    {
        private PlayerDetection PlayerDetection;
        public NavMeshAgent navMeshAgent;
        public float roamingRange;
        public taserShoot taser;
        public Animator guardAnimator;


        private AudioManager aM;

        private float timeBetweenShoots;

        private void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            PlayerDetection = GetComponent<PlayerDetection>();

            aM = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
            Debug.Log(aM);
        }
        private void setAnimator()
        {
            guardAnimator.SetFloat("velocityY", navMeshAgent.velocity.y);
            guardAnimator.SetFloat("velocityX", navMeshAgent.velocity.x);
        }
        private void Update()
        {

            setAnimator();
            if(PlayerDetection.seePlayer && PlayerDetection.inRange)
            {
                FaceTarget(PlayerDetection.player.position);
                timeBetweenShoots += Time.deltaTime;

                // Calculate the direction vector from the player to the agent
                Vector3 directionToPlayer = transform.position - PlayerDetection.player.position;
                directionToPlayer.y = 0f; // Ignore vertical difference

                // If the agent is too close to the player, set the destination to a point away from the player
                if (directionToPlayer.magnitude < 7)
                {
                    Vector3 desiredPosition = PlayerDetection.player.position + directionToPlayer.normalized * 7;
                    navMeshAgent.SetDestination(desiredPosition);
                }

                if (timeBetweenShoots > 1 && navMeshAgent.remainingDistance < 0.4f)
                {
                    guardAnimator.SetFloat("shooting", 1);

                    attackPlayer();
                    PlayerDetection.player.GetComponent<PlayerMovement>().playerHealth -= 5;
                    PlayerDetection.player.GetComponent<PlayerMovement>().updateHealth();
                    aM.Play("Player Hit");
                }

            }
            else if (PlayerDetection.seePlayer)
            {
                Vector3 directionToPlayer = transform.position - PlayerDetection.player.position;

                Vector3 desiredPosition = PlayerDetection.player.position + directionToPlayer.normalized * 7;

                navMeshAgent.SetDestination(desiredPosition);
                guardAnimator.SetFloat("shooting", 0);

            }
            else if (PlayerDetection.inHearing)
            {
                guardAnimator.SetFloat("shooting", 0);

                HeardPlayer();
            }
            else
            {
                guardAnimator.SetFloat("shooting", 0);

                roaming();
            }

            if (guardAnimator.GetFloat("shooting") == 1)
            {
                taser.particle1.gameObject.SetActive(true);
                taser.particle2.gameObject.SetActive(true);

            }
            else
            {
                taser.particle1.gameObject.SetActive(false);
                taser.particle2.gameObject.SetActive(false);

            }
        }

        private void roaming()
        {
            if (!PlayerDetection.seePlayer)
            {
                if (!navMeshAgent.hasPath || navMeshAgent.remainingDistance < 0.5f)
                {
                    Vector3 randomPosition = Random.insideUnitSphere * roamingRange;
                    NavMeshHit hit;
                    NavMesh.SamplePosition(transform.position + randomPosition, out hit, roamingRange, NavMesh.AllAreas);
                    navMeshAgent.SetDestination(hit.position);
                }
            }
        }
        private void HeardPlayer()
        {
            if (PlayerDetection.inHearing)
            {
                navMeshAgent.SetDestination(PlayerDetection.player.position);
                //PlayerDetection.overrideHeardingOff;
            }
        }
        private void attackPlayer()
        {
            if (PlayerDetection.inRange)
            {
                aM.Play("Taser");
            }           




            timeBetweenShoots = 0;


        }

        private void FaceTarget(Vector3 destination)
        {
            Vector3 lookPos = destination - transform.position;
            lookPos.y = 0;
            Quaternion rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 5 * Time.deltaTime);
        }
    }
}
