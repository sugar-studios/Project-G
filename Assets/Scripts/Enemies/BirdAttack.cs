using System.Collections;
using UnityEngine;
using ProjectG.UI;
using ProjectG.Manger;
using ProjectG.Player;
using ProjectG.Audio;

namespace ProjectG.Enemies
{
    public class BirdAttack : MonoBehaviour
    {
        public bool interrupt = false;
        private int score = 20;
        public MeterGauge meterGaugeScript;
        public GameManager gM;
        public PlaySound sound;
        public PlayerMovement pM;

        private void OnEnable()
        {
            Activate();
        }

        public void Activate()
        {
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
                yield return null;
            }


            if (!interrupt)
            {
                sound.sfx("Bird Attack");
                if (gM.DeleiveringMeal)
                {
                    for (float i = 0; i < 2f; i += 0.25f)
                    {
                        gM.value -= 2.5f;
                        if (gM.value < 1)
                        { 
                            gM.value = 1;
                        }
                        gM.UIManager.UpdateScore(gM.value);
                        yield return new WaitForSeconds(.25f);
                    }
                }
                else
                {
                    float nSpeed = pM.speed;
                    float SSpeed = pM.sprintSpeed;
                    float nSpeed2 = pM.speed/4;
                    float SSpeed2 = pM.sprintSpeed/4;
                    for (float i = 0; i < 2f; i += 0.25f)
                    {
                        yield return new WaitForSeconds(0.25f);
                        pM.speed = nSpeed2;
                        pM.sprintSpeed = SSpeed2;
                    }
                    pM.speed = nSpeed;
                    pM.sprintSpeed = SSpeed;
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
}
