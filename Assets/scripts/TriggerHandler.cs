using UnityEngine;

public class TriggerHandler : MonoBehaviour
{
    public GameManager GameManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.HandleTriggerEnter(gameObject.name);
        }
    }
}
