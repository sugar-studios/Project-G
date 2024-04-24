using ProjectG.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectG.Player
{
    public class PlayerGraphics : MonoBehaviour
    {
        public PlayerMovement pm;

        public PlaySound pS;

        public Animator modelAnim;
        public Animator itemAnim;

        private float sp;




        // Update is called once per frame
        void Update()
        {
            if (SceneManager.GetActiveScene().name == "Game Scene")
            { 
                modelAnim.SetFloat("Speed", pm.playerVelo);
                itemAnim.SetFloat("Speed", pm.playerVelo);
            }
        }

        public void PlayerHit()
        {
            modelAnim.SetTrigger("Hit");
        }

    }
}
