using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopUpDamageScript : MonoBehaviour
{
    public int damageValue;
    public Color32 playerColor;

    private void Start()
    {
        StartCoroutine(DestroyPopUp());
    }

    IEnumerator DestroyPopUp() {
        TextMeshPro txt = GetComponent<TextMeshPro>();

        txt.text = damageValue.ToString();
        txt.color = playerColor;
        yield return new WaitForSeconds(1f);

        Destroy(gameObject);
    }


}
