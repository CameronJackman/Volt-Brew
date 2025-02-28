using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public float moveSpeed = 1.5f;

    

    public float spawnCooldown = 2f;
    public float enemyDistance = 4;
    private bool isSeeking;

    public double amountOfCoinsDropped;
    public GameObject coinPrefab;

    private GameObject CoinsParent;



    private void Awake()
    {
        if (player == null || playerObj == null)
        {
           
            playerObj = GameObject.FindGameObjectWithTag("Player");
        }
        

        player = playerObj.transform;

        CoinsParent = GameObject.FindGameObjectWithTag("CurrentCoinsObj");
       
    }



    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") && damageCooldown <= 0f && !isDashing)
        {
                Health player = collision.gameObject.GetComponent<Health>();
                player.TakeDamage(contactDamage);
                damageCooldown = 1.0f;
        }

    }

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

    private void Update()
    {
        damageCooldown -= Time.deltaTime;

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
    }

    public void DropCoin()
    {
        GameObject coin;
        coin = Instantiate(coinPrefab, gameObject.transform);

        coin.transform.localScale = new Vector2(3.41f, 3.41f);
        coin.transform.parent = CoinsParent.transform;
        Debug.Log("Coin Dropped");

    }
}
