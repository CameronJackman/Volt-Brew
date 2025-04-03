using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float maxHealth = 100;

    [HideInInspector]
    public float currentHealth;
    public Spawner spawner;

    private DebugToggle _dt;

    private GameManager gameManager;

    private enemyWaveSpawns curEnemySpawnScpt;

    private Enemy enemyScript;

    [SerializeField]
    private GameObject enemyDeathParticle;

    [SerializeField]
    private GameObject playdeathAnimation;

    private Animations GameAnimations;

    [SerializeField]
    private AudioClip damageAudioClip;
    [SerializeField]
    private AudioClip deathAudioClip;



    void Start()
    {
        Time.timeScale = 1f;
        GameAnimations = FindAnyObjectByType<Animations>();
        currentHealth = maxHealth;
        
        gameManager = FindAnyObjectByType<GameManager>();

        if (gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            enemyScript = GetComponent<Enemy>();
        }
        
    }

    private void Awake()
    {
        spawner = FindObjectOfType<Spawner>();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        
        if (gameObject.CompareTag("Player"))
        {
            GameAnimations.playDamage = true;
        }

        if (gameObject.CompareTag("EnemyMelee") || gameObject.CompareTag("EnemyRange"))
        {
            GameAnimations.enemyAudioSource.PlayOneShot(damageAudioClip);
        }
            
    }

    private void Update()
    {
        //finding the current room spawns
        curEnemySpawnScpt = FindAnyObjectByType<enemyWaveSpawns>();

        if (currentHealth <= 0 && gameObject.CompareTag("Player"))
        {
            if (playdeathAnimation != null)
            {
                Time.timeScale = 0.0f;
                StartCoroutine(deathAnimation());
                
            }
            else
            {
                SceneManager.LoadScene(0);
                Debug.Log("Dead");
            }
                
        }

        else if (currentHealth <= 0 && gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if (curEnemySpawnScpt.currentAmtEnemys == 1 && curEnemySpawnScpt.enemiesToSpawn.Count == 0 && curEnemySpawnScpt.amountOfWaves == 0 && gameManager.storedPower != null)
            {
                enemyScript.DropPowerUp();
                curEnemySpawnScpt.currentAmtEnemys--;
                Instantiate(enemyDeathParticle, gameObject.transform.position, gameObject.transform.rotation);
                Destroy(gameObject);
            }
            else
            {
                GameAnimations.enemyAudioSource.PlayOneShot(deathAudioClip);
                curEnemySpawnScpt.currentAmtEnemys--;
                enemyScript.DropCoin();
                Destroy(gameObject);
            }
            
        }

      

    }

    IEnumerator deathAnimation()
    {
        playdeathAnimation.SetActive(true);

        yield return new WaitForSecondsRealtime(11);

        SceneManager.LoadScene(1);

    }


}



