using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ProjectileScript : MonoBehaviour
{
    public Rigidbody2D projectileRb;
    public float speed;

    public float projectileLife;
    public float projectileCount;

    public float projectileDamage;
    // Start is called before the first frame update
    void Start()
    {
        projectileCount = projectileLife;
    }

    // Update is called once per frame
    void Update()
    {
        projectileCount -= Time.deltaTime;

        if(projectileCount <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        //  projectileRb.velocity = projectileRb.AddRelativeForce(Vector3.forward);
        Vector3 direction3D = gameObject.transform.rotation * Vector3.right;
        Vector2 direction2D = new Vector2(direction3D.x, direction3D.y).normalized;

        projectileRb.AddForce(direction2D * speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Health enemyHeatlh = collision.gameObject.GetComponent<Health>();

            enemyHeatlh.TakeDamage(projectileDamage);
        }

        Destroy(gameObject);
    }

}
