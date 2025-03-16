using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class EnemyProjectileScript : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    private PlayerMovement2 _player;

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
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            _player = playerObj.GetComponent<PlayerMovement2>();
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
            StartCoroutine(DamageFeedback());
            
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Shield"))
        {
            
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("CombatZone"))
        {
            Destroy(gameObject);
        }

        //TO DESTROY ON WALLS
        //else if (collision.gameObject.CompareTag("Wall")) 
        //{
        //    Destroy(gameObject);
        //}


        //Destroys On collision with anything with collider
        Destroy(gameObject);
    }

    private IEnumerator DamageFeedback()
    {
        if (_player.sr != null)
        {
            _player.sr.color = Color.red;
            yield return new WaitForSeconds(0.2f);
            _player.sr.color = Color.white;
        }
        Health playerHealth = _player.GetComponent<Health>();
        playerHealth.TakeDamage(projectileDamage);

        Destroy(gameObject);
    }
}
