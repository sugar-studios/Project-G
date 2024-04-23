using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using ProjectG.Player;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    public TMP_Text totalScoreText;
    public TMP_Text scoreText;
    public PlayerMovement pM;
    public GameObject pause;
    public Slider healthBar;
    private Coroutine typingCoroutine;

    public void UpdateScore(int score)
    {
        totalScoreText.text = score.ToString();
    }

    public void OpenPauseMenu()
    { 
        pause.SetActive(true);
        UnityEngine.Cursor.visible = true;
        UnityEngine.Cursor.lockState = CursorLockMode.None;
    }

    private void Update()
    {
        healthBar.value = (int)pM.playerHealth;
    }

    public void ClosePauseMenu()
    {
        pause.SetActive(false);
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;
    }

    public void LeaveGame()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }

    public void TypeText(TMP_Text textBox, string textToType, float displayTime)
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        typingCoroutine = StartCoroutine(TypeTextRoutine(textBox, textToType, displayTime));
    }

    public void ClearAndStopTyping(TMP_Text textBox)
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        textBox.text = "";
    }

    private IEnumerator TypeTextRoutine(TMP_Text textBox, string textToType, float displayTime)
    {
        textBox.text = ""; 
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
