using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownScript : MonoBehaviour
{
    public float countDownTime;
    public TextMesh countDownText;

    void Start()
    {
        countDownText = countDownText.GetComponent<UnityEngine.TextMesh>();
        StartCoroutine(startCountdown());
    }

    IEnumerator startCountdown()
    {
        GameManager.Instance.EndGame();
        while (countDownTime > 0)
        {
            countDownText.text = countDownTime.ToString();

            yield return new WaitForSeconds(1f);

            countDownTime--;

        }

        countDownText.text = "GO!";
        GameManager.Instance.BeginGame();

        yield return new WaitForSeconds(1f);
        Object.Destroy(countDownText);

    }
}
