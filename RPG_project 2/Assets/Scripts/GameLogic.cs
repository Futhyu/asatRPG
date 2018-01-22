using UnityEngine;

public class GameLogic {

	public static float ExperienceForNextLevel(int currentLevel) {
        if (currentLevel <= 0)
            return 0;
        return (currentLevel*currentLevel * 10 + Mathf.Pow(2, currentLevel-1)*100);
    }

    public static float CalculatePlayerBaseAttackDamage(PlayerController player,PlayerStats thePS) {
        float baseDamage = thePS.strength.GetValue() / 10 * (1 + (thePS.Level / 10)) + EquipmentManager.GetDamage();
        return baseDamage;
    }
    
    public static float CalculateHealth(CharacterStats stats) {
        float health = stats.strength.GetValue() * stats.Level;
        return health;
    }

    public static float CalculateMana(CharacterStats stats) {
        float mana = stats.intelligence.GetValue() * stats.Level;
        return mana;
    }

    public static float CalculateExpForEnemy(EnemyStats enemy) {
        float exp = enemy.cost * (enemy.Level / PlayerStats.instance.Level);
        return exp;
    }
    public static float CalculateExpForQuest(Quest quest) {
        float exp = quest.reward.exp * (quest.requiredLevel / PlayerStats.instance.Level);
        return exp;
    }

    //public static float ChangeValueTo(float value, float modifier, float time) {
    //    float temp = 0;
    //    while(temp < time) {
    //        temp += Time.deltaTime;
    //        value -= modifier;
    //    }
    //    return value;
    //}
    
}
