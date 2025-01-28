using System.Collections;
using System.Collections.Generic;
using UnityEditor.Playables;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rb;
    public float maxHealth = 10.0f;
    float currentHealth = 10.0f;
    public int killPointVal = 100;

    Vector3 _dirToPlayer;
    bool calculatedThisFrame = false;

    public float enemySpeed = 5.0f;
    public float contactDamage = 0.5f;

    
    public float damageCooldown = 1.0f;

    

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") && damageCooldown <= 0f)
        {
            PlayerManager player = collision.gameObject.GetComponent<PlayerManager>();
            player.TakeDamage(contactDamage);
            damageCooldown = 1.0f;
            
        }

    }

    private void Update()
    {
        damageCooldown -= Time.deltaTime;
    }
}
