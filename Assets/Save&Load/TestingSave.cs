using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TestingSave : MonoBehaviour
{
    public Text level_Text;
    public int level;

    // Start is called before the first frame update
    public void Start()
    {
        level = 0;
        level_Text = GetComponent<Text>();
    }

    // Update is called once per frame
    public void Update()
    {
        level_Text.text = level.ToString();
    }

    public void AddLevel()
    {
        level += 1;
    }

    public void SubtractLevel()
    {
        level -= 1;
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayerr(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        level = data.level;
    }

}
