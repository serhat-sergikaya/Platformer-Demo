using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class JumpPad : MonoBehaviour
{
    Animator jumpPadAnimator;
    BoxCollider2D jumpPadCollider;
    void Start()
    {
        jumpPadAnimator = GetComponent<Animator>();
        jumpPadCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        AnimateBounce();
    }

    void AnimateBounce()
    {

        float timer = 5;
        if(jumpPadCollider.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            jumpPadAnimator.SetBool("isOn", true);
        }

        else
        {
         jumpPadAnimator.SetBool("isOn", false);

        }

    }
}
