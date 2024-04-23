using UnityEngine;

namespace ProjectG.Items
{

    [CreateAssetMenu(fileName = "New Item", menuName = "Item")]
    public class Item : ScriptableObject
    {
        public string name;
        public string description;
        public Texture icon;
        public GameObject model;


    }
}
