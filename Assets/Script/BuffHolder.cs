using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffHolder : MonoBehaviour
{
    private Buff _buff;

    public void SetBuff(Buff type) {
        _buff = type;
    }

    public void SetBuffValue(float value) {
        _buff.BuffValue = value;
    }

    public BuffType GetBuffType()
    {   
        return _buff.BuffType;
    }

    public float GetBuffValue() {
        return _buff.BuffValue;
    }

}
