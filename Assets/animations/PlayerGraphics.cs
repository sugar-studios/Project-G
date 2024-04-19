using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectG.Player
{
    public class PlayerGraphics : MonoBehaviour
    {
        public PlayerMovement pm;

        public Animator modelAnim;
        public Animator itemAnim;

        private float sp;


        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            modelAnim.SetFloat("Speed", pm.playerVelo);
            itemAnim.SetFloat("Speed", pm.playerVelo);
        }

        public void PlayerHit()
        {
            modelAnim.SetTrigger("Hit");
        }

    }
}
