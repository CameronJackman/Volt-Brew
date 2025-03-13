using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBeamShooting : MonoBehaviour
{
    public GameObject beamPrefab;
    public Transform firePoint;

    //Time between beams fired
    public float fireRate = 1.5f;

    //Time between beam 1 (no damage) and beam 2 (damage)
    public float timeBetweenBeams = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BeamRoutine());
    }

    private IEnumerator BeamRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(fireRate);
            ShootBeams();
        }
    }

    private void ShootBeams()
    {
        Instantiate(beamPrefab, firePoint.position, firePoint.rotation);

    }
}
