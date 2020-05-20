using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkingText : MonoBehaviour
{

    public Text blinkingText;
    private string temp;
    void Start()
    {
        blinkingText = GetComponent<Text>();
        temp = blinkingText.text;
        StartCoroutine(BlinkText());
    }

    //function to blink the text
    public IEnumerator BlinkText()
    {
        while (true)
        {
            blinkingText.text = "";
            yield return new WaitForSeconds(.5f);
            blinkingText.text = temp;
            yield return new WaitForSeconds(.5f);
        }
    }
}
