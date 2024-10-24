using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrophyController : MonoBehaviour
{

    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = anim.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //if player clicks on a trophy in inventory -- need to link to GameManager
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //spawn trophy in front of player

            //play spin animation

            //player presses E or something to stop it

            //delete trophy instance

            //inventory screen closes
        }
    }
}
