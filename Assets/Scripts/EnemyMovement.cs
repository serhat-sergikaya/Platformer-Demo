using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed =1f;
    Rigidbody2D enemyRb;
    BoxCollider2D enemyTriggerCollider;

    void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();
        enemyTriggerCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        enemyRb.velocity = new Vector2(moveSpeed, 0f);
    }

    private void OnTriggerExit2D(Collider2D other) {
        moveSpeed = -moveSpeed;
        FlipEnemySprite();
    }

    void FlipEnemySprite(){
        transform.localScale = new Vector2(-(Mathf.Sign(enemyRb.velocity.x)), 1f);
    }


}
