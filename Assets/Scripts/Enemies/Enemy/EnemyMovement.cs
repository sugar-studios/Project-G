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
        private NavMeshAgent navMeshAgent;
        public float roamingDistance;
        public float roamingRange;
        public taserShoot taser;

        private float timeBetweenShoots;

        private void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            PlayerDetection = GetComponent<PlayerDetection>();
        }

        private void Update()
        {

           
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

                if (timeBetweenShoots > 3)
                {
                    attackPlayer();
                }
                
            }
            else if (PlayerDetection.seePlayer)
            {
                Vector3 directionToPlayer = transform.position - PlayerDetection.player.position;

                Vector3 desiredPosition = PlayerDetection.player.position + directionToPlayer.normalized * 7;

                navMeshAgent.SetDestination(desiredPosition);
            }
            else if (PlayerDetection.inHearing)
            {
                HeardPlayer();
            }

            else
            {
                roaming();
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
            if (PlayerDetection.inHearing && !PlayerDetection.seePlayer)
            {
                navMeshAgent.SetDestination(PlayerDetection.player.position);
                //PlayerDetection.overrideHeardingOff;
            }
        }
        private void attackPlayer()
        {
            if (PlayerDetection.inRange)
            {
                //reduce playerHP
            }
            taser.shoot();
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
