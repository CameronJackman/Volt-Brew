using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParrallaxBackgroundEffect : MonoBehaviour
{
    private float startPos;
    private float startPosY;
    public GameObject cam;
    public float parallaxEffect;


    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position.x;
        startPosY = transform.position.y;


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float distance = cam.transform.position.x * parallaxEffect;
        float distance1 = cam.transform.position.y * parallaxEffect;

        

        transform.position = new Vector3(startPos + distance, startPosY + distance1, transform.position.z);
    }
}
