using UnityEngine;

public class EnemyStats : CharacterStats {
    
    public int id;
    new public string name;
    public float cost;
    
    void Awake() {
        damageBurst = Resources.Load<GameObject>("Prefabs/Utilities/DamageBurst");
        damageNum = Resources.Load<GameObject>("Prefabs/Utilities/DamageNumbers");
        buffs = new System.Collections.Generic.Queue<Buff>();
    }

    void Start() {
        SetStats();
    }
    
    void Update() {
        Activity();
    }
    
    protected override void Die() {
        if (PlayerData.activeQuests.Count > 0) {
            Quest.QuestKill questKill = PlayerData.monsterKilled.Find(x => x.name == name);
            if (questKill != null) {
                foreach (Quest quest in PlayerData.activeQuests) {
                    foreach (Quest.QuestKill qk in quest.task.kills) {
                        if (qk == questKill) {
                            questKill.amount++;
                        }
                    }
                }
            }
        }

        //DropLoot();

        Destroy(gameObject);

        PlayerStats.instance.SetExperience(cost);

    }

    public void ManaUse(float mana) {
        currentMana -= mana;
    }
    
    public float MaxHealth { get { return maxHealth; } }
    public float CurrentHealth { get { return currentHealth; } }

    public float MaxMana { get { return maxMana; } }
    public float CurrentMana { get { return currentMana; } }

}
