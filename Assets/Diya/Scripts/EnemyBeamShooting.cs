using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBeamShooting : MonoBehaviour
{
    public GameObject beamPrefab;
    public Transform firePoint;
    public float beamDuration = 2f;
    public float pauseTime = 1f;
    public float chargeTime = 0.5f;
    public float shootInterval = 3f;
    public Vector2 beamFinalSize = new Vector2(1f, 3f); // Adjust for how long the beam should extend

    private bool isShooting = false;
    private Enemy enemyMovement;

    void Start()
    {
        enemyMovement = GetComponent<Enemy>();
        StartCoroutine(AutoShoot());
    }

    private IEnumerator AutoShoot()
    {
        while (true)
        {
            StartShooting();
            yield return new WaitForSeconds(shootInterval);
        }
    }

    public void StartShooting()
    {
        if (!isShooting)
        {
            StartCoroutine(ShootBeam());
        }
    }

    private IEnumerator ShootBeam()
    {
        isShooting = true;

        if (enemyMovement != null)
            enemyMovement.enabled = false; // Freeze enemy

        yield return new WaitForSeconds(pauseTime);

        // Find the player
        Transform player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (player == null)
        {
            isShooting = false;
            if (enemyMovement != null)
                enemyMovement.enabled = true;
            yield break;
        }

        // Calculate direction to player
        Vector2 direction = (player.position - firePoint.position).normalized;
        float beamLength = Vector2.Distance(player.position, firePoint.position);

        // Instantiate beam at firePoint
        GameObject beam = Instantiate(beamPrefab, firePoint.position, Quaternion.identity);
        SpriteRenderer beamRenderer = beam.GetComponent<SpriteRenderer>();
        BeamDamage beamDamage = beam.GetComponent<BeamDamage>();
        BoxCollider2D beamCollider = beam.GetComponent<BoxCollider2D>();

        // Set beam opacity and disable damage
        Color beamColor = beamRenderer.color;
        beamColor.a = 0.2f;
        beamRenderer.color = beamColor;
        beamDamage.enabled = false;
        beamCollider.enabled = false;

        // Rotate beam towards player
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        beam.transform.rotation = Quaternion.Euler(0, 0, angle);

        // Set correct position (shift forward so it doesn’t appear behind the enemy)
        beam.transform.position += (Vector3)(direction * beamLength * 0.5f);

        // Beam starts small
        beam.transform.localScale = new Vector3(0.1f, 0.1f, 1f);

        // Grow beam over time
        float growDuration = chargeTime;
        float elapsedTime = 0f;

        while (elapsedTime < growDuration)
        {
            float progress = elapsedTime / growDuration;
            beam.transform.localScale = new Vector3(Mathf.Lerp(0.02f, beamLength, progress), 0.02f, 1f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Fully extend beam, increase opacity, and enable damage
        beam.transform.localScale = new Vector3(beamLength, 0.01f, 1f);
        beamColor.a = 1f;
        beamRenderer.color = beamColor;
        beamDamage.enabled = true;
        beamCollider.enabled = true;

        yield return new WaitForSeconds(beamDuration);

        Destroy(beam);

        if (enemyMovement != null)
            enemyMovement.enabled = true; // Allow enemy to move again

        isShooting = false;
    }
}