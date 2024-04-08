using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectG.Enemies.Handler
{
    public class EnemyTraffic : SpawnHandler
    {
        public UnityEngine.UI.Button.ButtonClickedEvent alertEnemies;

        public void checkNearby()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (Vector3.Distance(player.transform.position, transform.GetChild(i).position) < 30)
                {
                    //set that enemy to hostile and go to the player

                    //if none are nearby then the player is fine
                }
            }
        }

    }


#if UNITY_EDITOR
    [UnityEditor.CustomEditor(typeof(EnemyTraffic))]
    public class EnemySpawnerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EnemyTraffic spawner = (EnemyTraffic)target;

            if (GUILayout.Button("Spawn Enemy"))
            {
                spawner.SpawnEnemy(spawner.GetSpawnPoint());
            }
        }
    }
#endif

}
