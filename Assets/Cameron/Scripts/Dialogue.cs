using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI text;
    public TextMeshProUGUI skipText;
    public string[] lines;
    public float textSpeed;

    private int index;
    // Start is called before the first frame update
    void Start()
    {
        text.text = string.Empty;
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (text.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                text.text = lines[index];
                
            }
        }

        if (text.text == lines[index])
        {
            skipText.text = "Press Space To Continue";
        }
    }

    void StartDialogue()
    {
        skipText.text = "Press Space To Skip";
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            text.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            skipText.text = "Press Space To Skip";
            index++;
            text.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
