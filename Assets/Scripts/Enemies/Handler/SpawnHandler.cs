using ProjectG.Debugging;
using ProjectG.Enemies.Enemy;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace ProjectG.Enemies.Handler
{
    public class SpawnHandler : MonoBehaviour
    {
        [Header ("Spawn parameters")]
        public int totalEnemies;
        public bool alerted;
        public int initialEnemyCount = 5;
        public LayerMask groundMask;
        public GameObject guardPrefab;
        public GameObject player;
        public NavMeshSurface surface;

        private void Start()
        {
            SpawnInitialEnemies();
        }


        public void SpawnInitialEnemies()
        {
            for (int i = 0; i < initialEnemyCount; i++)
            {
                SpawnEnemy(RandomPointOnNavMesh());


            }
        }

        public void SpawnEnemy(Vector3 spawnPoint)
        {
            Instantiate(guardPrefab, spawnPoint, Quaternion.identity, transform);
            totalEnemies = transform.childCount;
        }

        Vector3 ValidatePoint(Vector3 point)
        {
            NavMeshHit hit;
            if (NavMesh.SamplePosition(point, out hit, 1.0f, NavMesh.AllAreas))
            {
                return hit.position;
            }
            else
            {
                // If the point is not valid, return a fallback point (e.g., the player's current position)
                return transform.position;
            }
        }

        public Vector3 RandomPointOnNavMesh()
        {
            NavMeshTriangulation navMeshData = NavMesh.CalculateTriangulation();

            int randomIndex = Random.Range(0, navMeshData.indices.Length - 3); // Random index for triangle
            int triangleIndex = randomIndex / 3; // Get the triangle index
            Vector3 point = Vector3.Lerp(navMeshData.vertices[navMeshData.indices[randomIndex]],
                                         navMeshData.vertices[navMeshData.indices[randomIndex + 1]],
                                         Random.value); // Random point within the triangle
            point = Vector3.Lerp(point, navMeshData.vertices[navMeshData.indices[randomIndex + 2]], Random.value); // Another random point within the triangle

            Vector3 validPoint = ValidatePoint(point);
            return ValidatePoint(point);
            //return point;
        }

        public void checkNearby()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (Vector3.Distance(player.transform.position, transform.GetChild(i).position) < PlayerStatesTester.PlayerNoiseRadius)
                {
                    //Debug.Log(transform.GetChild(i).name);

                    transform.GetChild(i).GetComponent<PlayerDetection>().inHearing = true;
                   
                }
            }
        }


#if UNITY_EDITOR
        [UnityEditor.CustomEditor(typeof(SpawnHandler))]
        public class EnemySpawnerEditor : UnityEditor.Editor
        {
            public override void OnInspectorGUI()
            {
                base.OnInspectorGUI();

                SpawnHandler spawner = (SpawnHandler)target;

                if (GUILayout.Button("Spawn Enemy"))
                {
                    spawner.SpawnEnemy(spawner.RandomPointOnNavMesh());
                }
            }
        }
#endif
    }
}
