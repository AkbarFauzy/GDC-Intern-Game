using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    public Slider fill;
    public Gradient grad;
    public Image fillImg;

    public void setMaxHealth(int hp) {
        fill.maxValue = hp;
        fill.value = hp;

        fillImg.color = grad.Evaluate(1f);
    }

    public void setHealth(int hp) {
        fill.value = hp;
        fillImg.color = grad.Evaluate(fill.normalizedValue);
    }

}
