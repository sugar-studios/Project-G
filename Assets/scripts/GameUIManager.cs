using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameUIManager : MonoBehaviour
{
    public TMP_Text tmpText; // Assign your TMP_Text component in the inspector

    public void TypeText(TMP_Text textBox, string textToType, float displayTime)
    {
        StartCoroutine(TypeTextRoutine(textBox, textToType, displayTime));
    }

    private IEnumerator TypeTextRoutine(TMP_Text textBox, string textToType, float displayTime)
    {
        textBox.text = ""; // Clear the text field before starting
        float typeSpeed = 0.05f;

        for (int i = 0; i <= textToType.Length; i++)
        {
            textBox.text = textToType.Substring(0, i);
            yield return new WaitForSeconds(typeSpeed);
        }

        yield return new WaitForSeconds(displayTime);

        textBox.text = "";
    }
}

