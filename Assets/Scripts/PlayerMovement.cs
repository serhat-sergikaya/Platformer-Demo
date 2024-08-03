using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2f;
    Vector2 moveInput;
    Rigidbody2D playerRb;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
    }

    private void OnMove(InputValue inputValue){
        moveInput = inputValue.Get<Vector2>();
    }

    private void Run(){

        Vector2 playerVelocity = new Vector2(moveInput.x* moveSpeed, playerRb.velocity.y);
        playerRb.velocity = playerVelocity;
    }
}
