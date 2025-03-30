using UnityEngine;
using System.Collections;

public class EnemyShooting : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;

    //Time between shots fired
    public float fireRate = 1.5f;

    private Animations GameAnimations;

    [SerializeField]
    private AudioClip shootingAudioClip;

    private void Start()
    {
        StartCoroutine(ShootRoutine());
        GameAnimations = FindAnyObjectByType<Animations>();
    }

    private IEnumerator ShootRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(fireRate);
            Shoot();
        }
    }

    private void Shoot()
    {
        GameAnimations.globalAudioSource.PlayOneShot(shootingAudioClip);
        Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
    }
}
