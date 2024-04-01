using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject biestro13;
    public GameObject adminOffice;
    public GameUIManager UIManager;

    private GameObject mealReceiveTrigger;
    private GameObject mealDeliverTrigger;

    private bool isReceiveTriggerActive = true;

    void Start()
    {
        GameObject player = Instantiate(playerPrefab, biestro13.transform.GetChild(0).transform.position, Quaternion.identity);

        mealReceiveTrigger = biestro13.transform.GetChild(1).gameObject;
        mealDeliverTrigger = adminOffice.transform.GetChild(1).gameObject;

        mealDeliverTrigger.GetComponent<Collider>().enabled = false;
    }

    public void HandleTriggerEnter(string triggerName)
    {
        if (triggerName == mealReceiveTrigger.name && isReceiveTriggerActive)
        {
            Debug.Log("Player entered the Meal Receive Trigger");
            isReceiveTriggerActive = false;

            mealReceiveTrigger.GetComponent<Collider>().enabled = false;
            mealDeliverTrigger.GetComponent<Collider>().enabled = true;
        }
        else if (triggerName == mealDeliverTrigger.name && !isReceiveTriggerActive)
        {
            Debug.Log("Player entered the Meal Deliver Trigger");
            isReceiveTriggerActive = true;

            mealReceiveTrigger.GetComponent<Collider>().enabled = true;
            mealDeliverTrigger.GetComponent<Collider>().enabled = false;
        }
    }
}
