using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class HealthPercentage : MonoBehaviour
{
    public Text txt;

    public void SetHealthPercentage(int maxHp, int currentHp)
    {
        int cal = Convert.ToInt32((float)currentHp / (float)maxHp * 100);

        txt.text = cal.ToString()+"%";
    }

}
