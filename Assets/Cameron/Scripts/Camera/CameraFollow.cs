using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField] private float FollowSpeed = 2f;
    [SerializeField] private Transform player;

    private Camera cam;
    Vector3 velocity = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        cam= GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = new Vector3(player.position.x, player.position.y, -10);

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, FollowSpeed);
    }
}
