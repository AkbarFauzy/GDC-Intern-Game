using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownScript : MonoBehaviour
{
    public float countDownTime;
    public Text countDownText;

    void Start()
    {
        countDownText = countDownText.GetComponent<UnityEngine.UI.Text>();
        StartCoroutine(startCountdown());
    }

    IEnumerator startCountdown()
    {
        while (countDownTime > 0)
        {
            countDownText.text = countDownTime.ToString();

            yield return new WaitForSeconds(1f);

            countDownTime--;

        }

        countDownText.text = "GO!";
        GameManager.Instance.BeginGame();

        yield return new WaitForSeconds(1f);
        DestroyObject(countDownText);

    }
}
