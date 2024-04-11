using UnityEngine;

public class TriggerHandler : MonoBehaviour
{
    public GameManager GameManager;

    [SerializeField] bool enableStay = false;

    private void Start()
    {
        GameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.HandleTriggerEnter(gameObject.name);
        }
        if (other.CompareTag("Player") && enableStay && this.gameObject.name == "Biestro Trigger")
        {
            GameManager.inBiestro = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && enableStay && this.gameObject.name == "Biestro Trigger")
        {
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && (this.gameObject.name == "Biestro Trigger"))
        {
            GameManager.inBiestro = false;
        }
        Debug.Log(gameObject.name);
        Debug.Log("HEY!");
    }
}
