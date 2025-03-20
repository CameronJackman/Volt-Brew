using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotScript : MonoBehaviour
{
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private GameObject bulletSpawn;
    [SerializeField]
    private SpriteRenderer robotGFX;

    

   
    
    // Start is called before the first frame update
    void Start()
    {
        
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

    public void Firing()
    {
        if (Time.deltaTime != 0)
        {
            Instantiate(bullet, bulletSpawn.transform.position, transform.rotation);
            
        }
        
    }

        
}
