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
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && enableStay)
        {
            GameManager.inBiestro = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && (this.gameObject.name == "Biestro HUB"))
        {
            GameManager.inBiestro = true;
        }
    }
}
