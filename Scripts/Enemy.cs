using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    
    public float speed = 2;
    private Vector2 target;
    private float distance;
    public float gravityEnemy;
    private bool isFalling;

    private bool isRunningAway;

    SignScript signScript;

    public float lifetime = 18;

    public int randomSound;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        signScript = GameObject.FindGameObjectWithTag("Sign").GetComponent<SignScript>();

        anim.SetBool("isRunning",true);
        anim.SetFloat("Speed",speed/2);

        // Set target
        target = new Vector2(0f, transform.position.y);
        if (transform.position.y > 0) target = new Vector2(2.8f, transform.position.y);
        else if (transform.position.y > -2 && transform.position.y <0) target = new Vector2(-2.8f, transform.position.y);

        //Set sound
        randomSound = Random.Range(0, 5)+1;


        // Flip if necessary
        if (transform.position.x > target.x)
        {
            Flip();
        }
    }

    
    void Update()
    {
        distance = Mathf.Abs(target.x - transform.position.x);
        if (distance > 0.2f)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

            // FALL DOWN
            if (transform.position.y > target.y)
                isFalling = true;
            else isFalling = false;                      

        }
        else if (distance < 0.2f && isRunningAway==false)
        {
            signScript.numHits++;
            Destroy(gameObject);
            
        }
        //-------------------------------------Destroy
        if (distance > 30 || lifetime <0)
        {
            Destroy(gameObject);            
        }
        lifetime -= Time.deltaTime;

        //----------------------------------Change target for some
        if (transform.position.y < -2 && transform.position.y > -4 && transform.position.x < 3)
            target.y = -6.1f;


    }

    private void FixedUpdate()
    {
        if (isFalling)
        {
            rb.AddForce(Vector2.down * gravityEnemy, ForceMode2D.Force);
        } else
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
    }

    void Flip()
    {
        
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isRunningAway == false)
        {
            speed *= 3;
            FindObjectOfType<AudioManager>().Play("EnemyHit"+randomSound.ToString());
            if (transform.position.x < target.x)
            {
                target = new Vector2(-20, transform.position.y);
                Flip();

                isRunningAway = true;
            }
            else if (transform.position.x > target.x)
            {
                target = new Vector2(20, transform.position.y);
                Flip();

                isRunningAway = true;
            }

            anim.SetBool("Hitted",true) ;
        }
    }
}
