using System.Collections;
using UnityEngine;

public class BirdAttack : MonoBehaviour
{
    public bool interrupt = false;
    public float initialY = 4f;
    public float finalY = 1f;
    private int score = 20;
    public MeterGauge meterGaugeScript;

    private void OnEnable()
    {
        Activate();
    }

    public void Activate()
    {
        transform.position = new Vector3(transform.position.x, initialY, transform.position.z);
        score = 20;
        Debug.Log("Initial Score: " + score);
        interrupt = false;

        StartCoroutine(MoveAndScore());
    }

    private IEnumerator MoveAndScore()
    {
        float startTime = Time.time;
        float endTime = startTime + 1f;
        while (Time.time < endTime)
        {
            if (interrupt)
            {
                yield break;
            }
            float t = (Time.time - startTime) / (endTime - startTime);
            float newY = Mathf.Lerp(initialY, finalY, t);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
            yield return null;
        }

        transform.position = new Vector3(transform.position.x, finalY, transform.position.z);

        if (!interrupt)
        {
            for (float i = 0; i < 2f; i += 0.25f)
            {
                yield return new WaitForSeconds(0.25f);
                score -= 2;
                Debug.Log("Score after deduction: " + score);
            }
        }

        ResetPositionAndDeactivate();
    }

    private void ResetPositionAndDeactivate()
    {
        gameObject.SetActive(false);
        interrupt = false;
        // Notify MeterGauge to restart its sequence
        meterGaugeScript.RestartSequence();
    }

    public void InterruptProcess()
    {
        interrupt = true;
    }
}
