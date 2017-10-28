using System.Collections;
using UnityEngine;

[System.Serializable]
public class Buff {

    public float value;
    public BuffType buffType;
    public float time;

    public enum BuffType { Health, Movement, Damage, Armor }
    
}
