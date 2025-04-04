using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Animations : MonoBehaviour
{

    [SerializeField]
    private GameObject doorAnimatiuon;
    [SerializeField]
    private GameObject waveAni;
    public TMP_Text waveCountText;
    [SerializeField]
    private GameObject damageAni;
    [SerializeField]
    private GameObject respawnAni;

    public bool playDoorTransition;
    public bool playWaveDisplay;
    public bool playDamage;
    public bool playRespawn;

    [HideInInspector]
    public bool transitionPlayed;

    [HideInInspector]
    public int waveCount;

    
    public AudioSource globalAudioSource;
    public AudioSource playerAudioSource;
    public AudioSource enemyAudioSource;

    [SerializeField]
    private AudioClip damageClip;
    
    public AudioClip pwrPickupClip;

    public AudioClip coinSfx;

    public AudioClip pwrDroppedClip;

    public AudioClip doorTransClip;

    public AudioClip itemBought;

    public AudioClip invalid;



    // Start is called before the first frame update
    void Start()
    {
        playRespawn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (playDoorTransition)
        {
            StartCoroutine(doorTransition());
        }

        if (playWaveDisplay)
        {
            StartCoroutine(waveAnimation());
        }

        if (playDamage)
        {
            StartCoroutine(damageAnimation());
        }

        if (playRespawn)
        {
            StartCoroutine(respawn());
        }

        
    }
    IEnumerator respawn()
    {
        respawnAni.SetActive(true);
        yield return new WaitForSeconds(10f);
        playRespawn = false;
        respawnAni.SetActive(false);
    }

    IEnumerator doorTransition()
    {
        playDoorTransition = false;
        transitionPlayed = true;
        doorAnimatiuon.SetActive(true);
        Debug.Log("Playing Door Transition");
        globalAudioSource.PlayOneShot(doorTransClip);
        yield return new WaitForSeconds(1.5f);
        
        doorAnimatiuon.SetActive(false);

    }

    IEnumerator waveAnimation()
    {
        playWaveDisplay = false;
        waveCountText.text = "Wave " + waveCount;
        waveAni.SetActive(true);
        yield return new WaitForSeconds(3f);
        waveAni.SetActive(false);

    }

    IEnumerator damageAnimation()
    {
        playerAudioSource.PlayOneShot(damageClip);

        playDamage = false;
        damageAni.SetActive(true);
        yield return new WaitForSeconds(1f);
        damageAni.SetActive(false);
    }


}
