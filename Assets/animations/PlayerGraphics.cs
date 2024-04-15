using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGraphics : MonoBehaviour
{
    public PlayerMovement pm;

    public Animator anim;

    private float sp;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("Speed", pm.playerVelo);
    }

    public void PlayerHit()
    {
        anim.SetTrigger("Hit");
    }

}
