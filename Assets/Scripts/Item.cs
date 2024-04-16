using UnityEngine;

[CreateAssetMenu( fileName = "New Item", menuName = "Item")]
public class Item : ScriptableObject
{
    public string name;
    public string description;
    public Sprite icon;
    public GameObject model;


}
