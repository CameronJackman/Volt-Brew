using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileScript : MonoBehaviour
{
    [SerializeField] private Health playerHealth;

    public Rigidbody2D enemyProjectileRb;
    public float speed = 5f;
    public float projectileLife = 3f;
    public float projectileDamage = 10f;

    private Transform player;
    private Vector2 direction;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (player != null)
        {
            direction = (player.position - transform.position).normalized;
        }
        else
        {
            //Fallback direction if no player found
            direction = transform.right;
        }

        //Destroy after projectileLife seconds
        Destroy(gameObject, projectileLife); 
    }

    void FixedUpdate()
    {
        enemyProjectileRb.velocity = direction * speed;
    }

    private void OnCollisionEnter2D (Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Health playerHealth = collision.gameObject.GetComponent<Health>();
            Debug.Log("Hit!");
            playerHealth.TakeDamage(projectileDamage);
            if (playerHealth != null)
            {
                Debug.Log("Playerhealth is Null");
            }
            Destroy(gameObject);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Shield"))
        {
            Debug.Log("Projectile collided with shield");
            Destroy(gameObject);
        }

        //TO DESTROY ON WALLS
        //else if (collision.gameObject.CompareTag("Wall")) 
        //{
        //    Destroy(gameObject);
        //}
    }
}
