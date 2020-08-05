using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopUpDamageScript : MonoBehaviour
{
    public int damageValue;
    public Color32 playerColor;
    private float speedY;
    private float timer;
    private void Start()
    {
        TextMeshPro txt = GetComponent<TextMeshPro>();
        txt.text = damageValue.ToString();
        txt.color = playerColor;
        speedY = 1f;
        timer = 0.5f;
    }

    private void Update()
    {
        transform.position += new Vector3(0, speedY, 0) * Time.deltaTime;
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            Destroy(gameObject);
        }
    }
}
