using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rb;
    public float contactDamage = 0.5f;
    public float damageCooldown = 1.0f;
    private PlayerMovement _player;
    private bool isDashing;




    private GameObject playerObj;
    private Transform player;
    private float playerPosition;




    public float spawnCooldown = 2f;
    public float enemyDistance = 4;
    private bool isSeeking;

    public double amountOfCoinsDropped;
    public GameObject coinPrefab;

    private GameObject CoinsParent;

    // Ai variables:
    private Transform target;
    public float moveSpeed = 200f;
    public float nextWayPointDistance = 3f;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath;
    Seeker seeker;

    public AIPath aiPath;

    // end AI Variables

    private GameObject curPower;

    private GameManager gameManager;

    private Animations GameAnimations;


    private void Awake()
    {
        GameAnimations = FindAnyObjectByType<Animations>();

        gameManager = FindAnyObjectByType<GameManager>();

        //ai pathfinding start
        target = GameObject.FindGameObjectWithTag("Player").transform;
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("UpdatePath", 0f, .5f);
        //end of Ai stuff

        

        if (player == null || playerObj == null)
        {

            playerObj = GameObject.FindGameObjectWithTag("Player");
        }


        player = playerObj.transform;

        CoinsParent = GameObject.FindGameObjectWithTag("CurrentCoinsObj");

    }


    void FixedUpdate()
    {
        //Ai Stuff



        //meleeEnemy
        /*  if (CompareTag("EnemyMelee"))
          {
              Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position);
              Vector2 force = (direction * moveSpeed * Time.deltaTime);


              rb.AddForce(force);
              float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
              if (distance < nextWayPointDistance)
              {
                  currentWaypoint++;  
              }
          } */

        if (CompareTag("EnemyRange"))
        {
            if (path == null)
            {
                return;
            }

            if (currentWaypoint >= path.vectorPath.Count)
            {
                reachedEndOfPath = true;
                return;
            }
            else
            {
                reachedEndOfPath = false;
            }


            float keepDistance = 1f;


            Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position);
            Vector2 force = (direction * moveSpeed * Time.deltaTime);
            float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

            if (distance > keepDistance)
            {
                rb.AddForce(force);
                
            }
            else if (distance < keepDistance)
            {
                rb.AddForce(-force);
            }

            if (distance < nextWayPointDistance)
            {
                currentWaypoint++;
            }

            // flips which way GFX is facing 

            if (force.x > 0.01f)
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else if (force.x < -0.01f)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }


        }



        if (CompareTag("EnemyMelee"))
        {
            //movement handled by A* ai movement


            // flips which way GFX is facing 
            if (aiPath.desiredVelocity.x > 0.01f)
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else if (aiPath.desiredVelocity.x < -0.01f)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
        }

        //End Ai Stuff

        // flips which way GFX is facing 






        /* OLD MOVEMENT OLD MOVEMENT OLD MOVEMENT OLD MOVEMENT OLD MOVEMENT OLD MOVEMENT 


        // Only Melee enemies seek
        if (CompareTag("EnemyMelee"))
        {
            SeekPlayer();
        }
        // Only Ranged enemies handle the "keep distance" logic
        else if (CompareTag("EnemyRange"))
        {
            RetreatPlayer();
        }
        OLD MOVEMENT OLD MOVEMENT OLD MOVEMENT OLD MOVEMENT OLD MOVEMENT OLD MOVEMENT OLD MOVEMENT  */



        damageCooldown -= Time.deltaTime;

        curPower = gameManager.storedPower;
    }





    //Ai PathFinding Stuff

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
        
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    //End of Ai stuff


    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") && damageCooldown <= 0f && !isDashing)
        {
                Health player = collision.gameObject.GetComponent<Health>();
                player.TakeDamage(contactDamage);
                damageCooldown = 1.0f;
        }

    }
   



    /*
    void SeekPlayer()
    {

        Vector2 moveToPlayer = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        transform.position = moveToPlayer;
        isSeeking = true;

    }

    void RetreatPlayer()
    {
        
        {
            float keepDistance = 1f;
            Vector2 playerPosition = player.position;
            float distance = Vector2.Distance(playerPosition, transform.position);

            if (distance < keepDistance)
            {
                isSeeking = false;
                Vector2 direction = ((Vector2)transform.position - playerPosition).normalized;
                float step = moveSpeed * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, (Vector2)transform.position + direction, step);
                

            }
            else
            {
                Vector2 direction = (playerPosition - (Vector2)transform.position).normalized;
                float step = moveSpeed * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, (Vector2)transform.position + direction, step);
                isSeeking = true;
            }
        }
    }
    */


    public void DropCoin()
    {
        GameObject coin;
        coin = Instantiate(coinPrefab, gameObject.transform);

        
        coin.transform.SetParent(CoinsParent.transform, true);
        coin.transform.localScale = new Vector2(2.32f, 2.32f);
        Debug.Log("Coin Dropped");

    }

    public void DropPowerUp()
    {
        GameAnimations.enemyAudioSource.PlayOneShot(GameAnimations.pwrDroppedClip);
        GameObject PowerUpCollectable = Instantiate(curPower, gameObject.transform);
        PowerUpCollectable.transform.SetParent(CoinsParent.transform, true);
        PowerUpCollectable.transform.localScale = new Vector2(0.09f, 0.09f);
        Debug.Log("PowerUp Dropped");
    }
}
