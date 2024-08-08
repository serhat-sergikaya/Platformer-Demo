using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    const string IS_RUNNING = "isRunning";
    const string IS_JUMPING = "isJumping";

    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(5f,5f);
    Vector2 moveInput;
    Rigidbody2D playerRb;
    CapsuleCollider2D playerBodyCollider;
    BoxCollider2D playerFeetCollider;
    Animator playerAnimator;


    bool isAlive = true;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerBodyCollider = GetComponent<CapsuleCollider2D>();
        playerFeetCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isAlive) {return;}
        Run();
        FlipSprite();
        PlayerAnim();
        Die();
    }

    void OnMove(InputValue inputValue)
    {
        moveInput = inputValue.Get<Vector2>();
    }

    void PlayerAnim(){

        if(!playerFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
                playerAnimator.SetBool(IS_JUMPING, true );
                playerAnimator.SetBool(IS_RUNNING, false);

        }

        if(playerFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
                playerAnimator.SetBool(IS_JUMPING, false);


                //Fire run animation

                bool playerHasHorizontalMovement = Mathf.Abs(playerRb.velocity.x) > Mathf.Epsilon;

                playerAnimator.SetBool(IS_RUNNING, playerHasHorizontalMovement);

        }
        


    }
    void OnJump(InputValue inputValue)
    {

        if(playerFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            if(inputValue.isPressed)
            {
                playerRb.velocity  += new Vector2(0f, jumpSpeed);
            }
        }

    }

    void Run()
    {

        Vector2 playerVelocity = new Vector2(moveInput.x* moveSpeed, playerRb.velocity.y);

         playerRb.velocity = playerVelocity;
    


    }

    void FlipSprite()
    {
        bool playerHasHorizontalMovement = Mathf.Abs(playerRb.velocity.x) > Mathf.Epsilon;

        if(playerHasHorizontalMovement)
        {
            transform.localScale = new Vector2(Mathf.Sign(playerRb.velocity.x), 1f);
        }
    }

    void Die()
    {
        if(playerBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy")) || playerFeetCollider.IsTouchingLayers(LayerMask.GetMask("Enemy"))  )
        {
            isAlive = false;
            playerAnimator.SetTrigger("Death");

            playerRb.velocity = deathKick;
        }
    }
}
