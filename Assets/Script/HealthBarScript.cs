using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    public Slider fill;
    public Gradient grad;
    public Image fillImg;

    public void setMaxHealth(int hp, int maxHealth) {
        fill.maxValue = maxHealth;
        fill.value = hp;

        fillImg.color = grad.Evaluate(1f);
    }

    public void SetMaxHealthBoss(int hp)
    {
        fill.maxValue = GameManager.BOSSMAXHEALTH;
        fill.value = hp;

        fillImg.color = grad.Evaluate(1f);
    }

    public void SetExtraMaxHealth(int hp)
    {
        fill.maxValue = 400;
        fill.value = hp;

        fillImg.color = grad.Evaluate(1f);
    }

    public void SetHealth(int hp) {
        fill.value = hp;
        fillImg.color = grad.Evaluate(fill.normalizedValue);
    }

}
