using ProjectG.Enemies.Handler;
using ProjectG.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectG.Items
{
    public class itemUsageMethods : MonoBehaviour
    {
        public void useTest()
        {
            Debug.Log("use item");
        }

        public void panUse()
        {
            Debug.Log("used pan");
            FindFirstObjectByType<SpawnHandler>().checkNearby();

        }
    }
}
