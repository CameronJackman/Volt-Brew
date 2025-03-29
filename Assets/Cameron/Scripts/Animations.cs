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

    public bool playDoorTransition;
    public bool playWaveDisplay;
    public bool playDamage;

    [HideInInspector]
    public int waveCount;



    // Start is called before the first frame update
    void Start()
    {
        
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
    }

    IEnumerator doorTransition()
    {

        doorAnimatiuon.SetActive(true);

        yield return new WaitForSeconds(1.5f);
        playDoorTransition = false;
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
        playDamage = false;
        damageAni.SetActive(true);
        yield return new WaitForSeconds(1f);
        damageAni.SetActive(false);
    }


}
