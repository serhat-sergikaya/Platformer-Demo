using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    const string IS_RUNNING = "isRunning";

    [SerializeField] float moveSpeed = 5f;
    Vector2 moveInput;
    Rigidbody2D playerRb;

    Animator playerAnimator;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        FlipSprite();
    }

    void OnMove(InputValue inputValue)
    {
        moveInput = inputValue.Get<Vector2>();
    }

    void Run()
    {

        Vector2 playerVelocity = new Vector2(moveInput.x* moveSpeed, playerRb.velocity.y);
        playerRb.velocity = playerVelocity;

        bool playerHasHorizontalMovement = Mathf.Abs(playerRb.velocity.x) > Mathf.Epsilon;

        //Fire run animation

        playerAnimator.SetBool(IS_RUNNING, playerHasHorizontalMovement);
    }

    void FlipSprite()
    {
        bool playerHasHorizontalMovement = Mathf.Abs(playerRb.velocity.x) > Mathf.Epsilon;

        if(playerHasHorizontalMovement)
        {
            transform.localScale = new Vector2(Mathf.Sign(playerRb.velocity.x), 1f);
        }
    }
}
