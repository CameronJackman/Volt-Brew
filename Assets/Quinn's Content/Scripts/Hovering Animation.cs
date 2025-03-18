using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoveringAnimation : MonoBehaviour
{

    public float amp;
    public float freq;
    Vector3 initpos;


    private void Start()
    {
        initpos = transform.position;
    }
    // Update is called once per frame
    private void Update()
    {
        transform.position = new Vector3(initpos.x, Mathf.Sin(Time.time) * amp, initpos.z); 
    }
}
