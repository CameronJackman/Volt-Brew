using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rb;
    private SpriteRenderer sr;

    public float contactDamage = 0.5f;
    public float damageCooldown = 0.0f;
    [SerializeField] private PlayerMovement2 _player;
    private bool isDashing;

    private GameObject playerObj;
    private Transform player;
    private float playerPosition;
    public float moveSpeed = 1.5f;

    public float spawnCooldown = 2f;
    public float enemyDistance = 4;
    private bool isSeeking;

    public float knockbackForce;  // Set high (e.g. 500) in Inspector

    private void Awake()
    {
        if (player == null || playerObj == null)
        {
            playerObj = GameObject.FindGameObjectWithTag("Player");
        }
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        if (_player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                _player = playerObj.GetComponent<PlayerMovement2>();
            }
        }
        player = playerObj.transform;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") && damageCooldown <= 0f && !isDashing)
        {
            StartCoroutine(FlashRed());
            Health playerHealth = collision.gameObject.GetComponent<Health>();
            playerHealth.TakeDamage(contactDamage);
            damageCooldown = 1.0f;
        }
    }

    private IEnumerator FlashRed()
    {
        _player.sr.color = Color.red;
        yield return new WaitForSeconds(0.2f); // flash duration
        _player.sr.color = Color.white;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            // Instead of directly applying force here, use the player's ApplyKnockback method.
            Vector2 knockbackDirection = (player.position - transform.position).normalized;
            _player.ApplyKnockback(knockbackDirection * knockbackForce);

            Debug.Log("Player Knocked Back With " + (knockbackForce * knockbackDirection) + " strength");
            Debug.Log(knockbackDirection);
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
}