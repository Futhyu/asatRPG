[System.Serializable]
public class Buff {

    public float value;
    public BuffType type;
    public float time;

    public enum BuffType { DamageOverTime, Movement, DamageIncreasing, Armor, Stun, Healing }
    
}
