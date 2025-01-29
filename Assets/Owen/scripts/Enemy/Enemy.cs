using System.Collections;
using System.Collections.Generic;
using UnityEditor.Playables;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rb;
    public float contactDamage = 0.5f;
    public float damageCooldown = 1.0f;
    private PlayerMovement _player;
    private bool isDashing;

    public Transform player;
    private float playerPosition;
    public float moveSpeed = 3;
    public float spawnCooldown = 2f;



   
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
    }

    private void Update()
    {
        damageCooldown -= Time.deltaTime;
        SeekPlayer();
    }
}
