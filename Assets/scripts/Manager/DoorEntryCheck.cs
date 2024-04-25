using ProjectG.Audio;
using ProjectG.Manger;
using ProjectG.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectG.Manager
{
    public class DoorEntryCheck : MonoBehaviour
    {
        public char Enter = '0';
        public GameManager gM;
        public PlayerMovement pM;
        public PlaySound pS;
        public void yes()
        {
            float testVal = gM.value;
            testVal -= 5;
            if (testVal < 1)
            {
                pS.sfx("Error");
                no();
                return;
            }
            gM.value -= 5;
            Enter = '1';
            pM.playerHealth += 10;
            pM.currentStamina += pM.maxStamina *.10f;
            gM.UIManager.UpdateScore(gM.value);

            //remove 15% from bird meter and disable it for 3 seconds
        }
        public void no()
        {
            Enter = '2';
        }


        private void OnDisable()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void OnEnable()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        private void Update()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
