using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEXP : MonoBehaviour
{
    public int exp;
    public Text exp_Text;

    // Start is called before the first frame update
    void Start()
    {
        exp = 100;
        exp_Text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        exp_Text.text = exp.ToString();
    }
    public void AddEXP()
    {
        exp += 150;
    }

    public void SubtractEXP()
    {
        exp -= 25;
    }

    public void SavePlayerEXP()
    {
        SaveSystem.SavePlayer(FindObjectOfType<TestingSave>(), this);
    }

    public void LoadPlayerEXP()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        if (data != null)
        {
            exp = data.exp;
        }
    }
}
