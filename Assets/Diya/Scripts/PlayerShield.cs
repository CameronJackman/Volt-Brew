using UnityEngine;
using System.Collections;

public class PlayerShield : MonoBehaviour
{
    public GameObject shieldPrefab; 
    private GameObject activeShield;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && activeShield == null)
        {
            ActivateShield();
        }
    }

    void ActivateShield()
    {
        activeShield = Instantiate(shieldPrefab, transform.position, Quaternion.identity);

        //Set proper scale of shield 
        activeShield.transform.localScale = Vector3.one * 0.4f;  

        //Start coroutine to follow player
        StartCoroutine(FollowPlayerForSeconds(5f));
    }

    IEnumerator FollowPlayerForSeconds(float seconds)
    {
        float timer = 0f;
        while (timer < seconds)
        {
            if (activeShield != null)
            {
                //Keeps shield on player
                activeShield.transform.position = transform.position;  
            }
            timer += Time.deltaTime;
            yield return null;
        }

        if (activeShield != null)
        {
            Destroy(activeShield);
        }
    }
}