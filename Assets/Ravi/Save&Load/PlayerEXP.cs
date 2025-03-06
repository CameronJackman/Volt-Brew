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
        exp = 12338;
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
}
