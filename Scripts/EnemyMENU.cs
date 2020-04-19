using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMENU : MonoBehaviour
{
    public float speed;
    Transform playerPos;
    private float distance;
    private float distanceFromPlayer;

    private bool isFacingRight=true;

    Rigidbody2D rb;
    Animator anim;
    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        //playerRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

        //--------------------------------------------MOVEMENT
        Vector2 target = playerPos.position;
        
        if (playerPos.position.x < transform.position.x)
        {
            target.x += 4;
            if (isFacingRight==false)
                Flip();
            isFacingRight = true;
        }
        else
        {
            target.x -= 4;
            if (isFacingRight == true)
                Flip();
            isFacingRight = false;

        }
        distance = Mathf.Abs(target.x - transform.position.x);
        distanceFromPlayer = Mathf.Abs(playerPos.position.x - transform.position.x);

        

        if (distance > 0.1f &&  distanceFromPlayer < 2f)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
            anim.SetBool("isRunning", true);
        }
        else
            anim.SetBool("isRunning", false);
    }

    void Flip()
    {

        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;

    }
}
