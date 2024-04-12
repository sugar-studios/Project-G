using UnityEngine;

public class TriggerHandler : MonoBehaviour
{
    public GameManager GameManager;

    [SerializeField] bool enableStay = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.HandleTriggerEnter(gameObject.name);
        }
        if (other.CompareTag("Player") && enableStay && this.gameObject.name == "Biestro Trigger")
        {
            GameManager.isInBiestro = true;
        }
    }
}
