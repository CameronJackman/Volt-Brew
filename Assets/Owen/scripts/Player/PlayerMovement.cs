using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    public float moveSpeed = 1.5f;
    

    public float acceleration = 11f;   
    public float deceleration = 15f;

    //Dash Settings
    public float dashDuration = 10f;
    public float dashSpeedMultiplier = 3f;
    public float dashCoolDownTime = 1.5f;

    private float dashCoolDownTimer = 0f;

    private Vector2 isoRight = new Vector2(-1.75f, 1.0f);
    private Vector2 isoUp = new Vector2(1.75f, 1.0f);

    private Rigidbody2D rb;

    private Vector2 currentVelocity = Vector2.zero;
    private Vector2 rawInput = Vector2.zero;


    public bool isDashing = false;
    private bool spawnLeft = true;
  

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
       
    }

    void Update()
    {

        if (dashCoolDownTimer > 0)
        {
            dashCoolDownTimer -= Time.deltaTime;
        }
       
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        rawInput = new Vector2(horizontal, vertical).normalized;


        if (Input.GetKeyDown(KeyCode.LeftShift) && dashCoolDownTimer <= 0f && !isDashing)
        {
            StartCoroutine(PerformDash());
        }

    }

    void FixedUpdate()
    {

        if (isDashing) return;


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

    }

    private IEnumerator PerformDash()
    {
        isDashing = true;

        Vector2 originalVelocity = rb.velocity;
        rb.velocity = originalVelocity * dashSpeedMultiplier;

        yield return new WaitForSeconds(dashDuration);

        isDashing = false;
        rb.velocity = originalVelocity;

        dashCoolDownTimer = dashCoolDownTime;
    }


    private void OnTriggerEnter2D(Collider2D collision)
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

