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
        public Vector2 spawnBounds;
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

        public Vector3 RandomPointOnNavMesh()
        {
            NavMeshTriangulation navMeshData = NavMesh.CalculateTriangulation();

            int randomIndex = Random.Range(0, navMeshData.indices.Length - 3); // Random index for triangle
            int triangleIndex = randomIndex / 3; // Get the triangle index
            Vector3 point = Vector3.Lerp(navMeshData.vertices[navMeshData.indices[randomIndex]],
                                         navMeshData.vertices[navMeshData.indices[randomIndex + 1]],
                                         Random.value); // Random point within the triangle
            point = Vector3.Lerp(point, navMeshData.vertices[navMeshData.indices[randomIndex + 2]], Random.value); // Another random point within the triangle

            return point;
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
