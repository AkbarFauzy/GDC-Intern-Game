using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopUpDamageScript : MonoBehaviour
{
    public int DamageValue { get; private set; }
    public Color32 PopUpColor { get; private set; }
    private float _speedY;
    private float _timer;

    private void Start()
    {
        TextMeshPro txt = GetComponent<TextMeshPro>();
        txt.text = DamageValue.ToString();
        txt.color = PopUpColor;
        _speedY = 1f;
        _timer = 0.5f;
    }

    private void Update()
    {
        transform.position += new Vector3(0, _speedY, 0) * Time.deltaTime;
        _timer -= Time.deltaTime;

        if (_timer <= 0)
        {
            gameObject.SetActive(false);
            _timer = 0.5f;
        }
    }

    public void SetDamageValue(int damage) {
        DamageValue = damage;  
    }

    public void SetColor(Color32 color)
    {
        PopUpColor = color;
    }

}
