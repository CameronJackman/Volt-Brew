using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    public float moveSpeed = 1.5f;
    private float sprintMultiplier = 1.5f;
    public float dashCooldown = 2.0f;

    public float acceleration = 11.61f;   
    public float deceleration = 11.1f;   

    private Vector2 isoRight = new Vector2(-1.75f, 1.0f);
    private Vector2 isoUp = new Vector2(1.75f, 1.0f);

    private Rigidbody2D rb;

    private Vector2 currentVelocity = Vector2.zero;
    private Vector2 rawInput = Vector2.zero;

    private bool isSprinting;

    private bool spawnLeft = true;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
       
    }

    void Update()
    {
       
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        
        rawInput = new Vector2(horizontal, vertical).normalized;

    }

    void FixedUpdate()
    {
        
        Vector2 desiredMove = rawInput.x * -isoRight + rawInput.y * isoUp;

      
        Vector2 targetVelocity = desiredMove * moveSpeed;

        
        if (desiredMove != Vector2.zero)
        {
           
            currentVelocity = Vector2.MoveTowards(
                currentVelocity,
                targetVelocity,
                acceleration * Time.fixedDeltaTime
            );
        }
        else
        {
            
            currentVelocity = Vector2.MoveTowards(
                currentVelocity,
                Vector2.zero,
                deceleration * Time.fixedDeltaTime
            );
        }

        
        rb.velocity = currentVelocity;




        // Player Dash
        if (Input.GetKey(KeyCode.LeftShift) && dashCooldown <= 0)
        {
            currentVelocity *= 2f;
            dashCooldown = 1.5f;
            isSprinting = true;
        }
        else if (isSprinting == true && Input.GetKeyUp(KeyCode.LeftShift)) 
        {
            isSprinting = false;
            currentVelocity /= 2f;
        }

        

    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("leftTrig"))
        {
            spawnLeft = true;
        }

        if (collision.CompareTag("rightTrig"))
        {
            spawnLeft = false;
        }
    }

    public bool spawnLocationCheck()
    {
        return spawnLeft;
    }

    public void setLocation(GameObject targetLocation)
    {
        gameObject.transform.position = targetLocation.transform.position;
    }

  
}

