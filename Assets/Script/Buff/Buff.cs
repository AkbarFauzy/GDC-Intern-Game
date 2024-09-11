using System;

public enum BuffType { 
    None,
    BuffSpeed,
    BuffDamage,
    BuffHealth,
    DebuffSpeed,
    DebuffDamage,
    DebuffHealth,
    Life,
}

[Serializable]
public struct Buff
{
    public BuffType BuffType;
    public float BuffValue;

    public Buff(BuffType type, float value)
    {
        BuffType = type;
        BuffValue = value;
    }
}
