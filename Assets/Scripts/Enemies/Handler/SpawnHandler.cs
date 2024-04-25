using ProjectG.Debugging;
using ProjectG.Enemies.Enemy;
using Unity.AI.Navigation;
using Unity.VisualScripting;
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
        public Vector2 spawnBounds;
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
                Vector3 point;

                if (RandomPoint(randomPoint(), range, out point))
                {
                    SpawnEnemy(point);
                }
                else
                {
                    
                    Debug.Log("not valid point, press again");
                    continue;

                }
            }
        }

        public void SpawnEnemy(Vector3 spawnPoint)
        {
            Instantiate(guardPrefab, spawnPoint, Quaternion.identity, transform);
            totalEnemies = transform.childCount;
        }

        public Vector3 randomPoint()
        {
            Vector3 point = new Vector3 (0, 0, 0);
            point.x = Random.Range(-spawnBounds.x, spawnBounds.x);
            point.z = Random.Range(-spawnBounds.y,  spawnBounds.y);

            return Vector3.zero;
        }
        
        public float range = 15.0f;
        public bool RandomPoint(Vector3 center, float range, out Vector3 result)
        {
            for (int i = 0; i < 30; i++)
            {
                Vector3 randomPoint = center + Random.insideUnitSphere * range;
                NavMeshHit hit;
                if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
                {
                    result = hit.position;
                    return true;
                }
            }
            result = Vector3.zero;
            return false;
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
                    Vector3 point = new Vector3();

                    if (spawner.RandomPoint(spawner.randomPoint(), spawner.range, out point))
                    {
                        spawner.SpawnEnemy(point);
                    }
                    else
                    {
                        Debug.Log("not valid point, press again");
                    }
                }
            }
        }
#endif
    }
}
