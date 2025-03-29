using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotScript : MonoBehaviour
{
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private GameObject rapidFireBullet;
    [SerializeField]
    private GameObject shotgunBullet;
    [SerializeField]
    private GameObject bulletSpawn;
    [SerializeField]
    private SpriteRenderer robotGFX;
    [SerializeField]
    private GameObject bulletAudio;
    [SerializeField]
    private GameObject shotgunAudio;

    public bool shotGun;

    public bool rapid;

    private PlayerMovement2 playerScpt;

    [HideInInspector]
    public float currentDamage;

    
    
    // Start is called before the first frame update
    void Start()
    {
        playerScpt = FindAnyObjectByType<PlayerMovement2>();
        playerScpt.resetCooldownCount = 0.5f;
    }

    // Update is called once per frame
    

    public void AimAtScreenPosition(Vector2 screenPos)
    {
        if (Time.deltaTime != 0)
        {
            float distanceToCamera = Mathf.Abs(Camera.main.transform.position.z);
            Vector3 screenPos3D = new Vector3(screenPos.x, screenPos.y, distanceToCamera);
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos3D);
            worldPos.z = 0f;
            Vector3 direction = worldPos - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);

            if (angle <= 90 && angle >= -90)
            {
                robotGFX.flipY = false;
            }
            else
            {
                robotGFX.flipY = true;
            }
        }
    }

    public void AimWithController(Vector2 direction)
    {

        if (Time.deltaTime != 0)
        {
            // direction is a normalized or near-normalized stick vector, e.g. (0.6, -0.4)
            if (direction.sqrMagnitude > 0.001f) // if not basically zero
            {
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }
        }
           
    }

    private IEnumerator FireBurst()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject newbullet = Instantiate(rapidFireBullet, bulletSpawn.transform.position, transform.rotation);

            ProjectileScript bulletProject = newbullet.GetComponent<ProjectileScript>();

            bulletProject.projectileDamage = 20;
            currentDamage = bulletProject.projectileDamage;

            GameObject Audio4Bullet = Instantiate(bulletAudio, bulletSpawn.transform.position, transform.rotation);
            Destroy(Audio4Bullet, 4.3f);

            yield return new WaitForSeconds(0.2f);
        }
    }

    public void Firing()
    {
        if (Time.deltaTime != 0)
        {
            if (shotGun)
            {
                for (int i = 0; i < 3; i++)
                {
                    float randomAngle = Random.Range(-300, 300);
                    GameObject newbullet = Instantiate(shotgunBullet, bulletSpawn.transform.position, transform.rotation);

                    Vector2 direction = Quaternion.Euler(0, 0, randomAngle) * bulletSpawn.transform.right;

                    Rigidbody2D rb = newbullet.GetComponent<Rigidbody2D>();
                    rb.AddForce(direction, ForceMode2D.Impulse);

                    ProjectileScript bulletProject = newbullet.GetComponent<ProjectileScript>();

                    bulletProject.projectileDamage = 20;
                    currentDamage = bulletProject.projectileDamage;
                }

                GameObject Audio4Bullet = Instantiate(shotgunAudio, bulletSpawn.transform.position, transform.rotation);
                Destroy(Audio4Bullet, 4.3f);
                
            }
            if (rapid)
            {
                StartCoroutine(FireBurst());
                
            }
            else if (!rapid && !shotGun)
            {
                //bullet
                GameObject newbullet = Instantiate(bullet, bulletSpawn.transform.position, transform.rotation);
                ProjectileScript bulletProject = newbullet.GetComponent<ProjectileScript>();
                currentDamage = bulletProject.projectileDamage;
                //bullet audio
                GameObject Audio4Bullet = Instantiate(bulletAudio, bulletSpawn.transform.position, transform.rotation);
                Destroy(Audio4Bullet, 4.3f);
                
            }

           
        }
        
    }

        
}
