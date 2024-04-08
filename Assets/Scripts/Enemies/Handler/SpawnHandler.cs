using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectG.Enemies.Handler
{
    public class SpawnHandler : MonoBehaviour
    {
        public int totalEnemies;
        public bool alerted;
        public int initialEnemyCount = 5;
        public Vector2 spawnBounds;
        public LayerMask groundMask;
        public GameObject guardPrefab;
        public GameObject player;

        private void Start()
        {
            SpawnInitialEnemies();
        }

        public Vector3 GetSpawnPoint()
        {
            while (true)
            {
                float x = Random.Range(-spawnBounds.x, spawnBounds.x);
                float z = Random.Range(-spawnBounds.y, spawnBounds.y);

                RaycastHit hit;

                if (Physics.Raycast(new Vector3(x, transform.position.y, z), Vector3.down, out hit, 200, groundMask))
                {
                    if (Vector3.Distance(hit.point, player.transform.position) > 50)
                    {
                        return hit.point;
                    }
                }
            }
        }

        public void SpawnInitialEnemies()
        {
            for (int i = 0; i < initialEnemyCount; i++)
            {
                SpawnEnemy(GetSpawnPoint());
            }
        }

        public void SpawnEnemy(Vector3 spawnPoint)
        {
            Instantiate(guardPrefab, spawnPoint, Quaternion.identity, transform);
            totalEnemies++;
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
                    spawner.SpawnEnemy(spawner.GetSpawnPoint());
                }
            }
        }
#endif
    }
}
