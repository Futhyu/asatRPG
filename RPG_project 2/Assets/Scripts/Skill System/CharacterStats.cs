using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour {
    
    [SerializeField]
    protected int level;
    protected float experience;

    protected Queue<Buff> buffs;

    public float maxHealth;
    public float currentHealth { get; protected set; }

    public float maxMana;
    public float currentMana { get; protected set; }
    
    protected PlayerController thePlayer;
    
    protected GameObject damageBurst;
    protected GameObject damageNum;

    public Stat damage;
    public Stat armor;
    public Stat intelligence;
    public Stat strength;
    
    void Awake() {
        thePlayer = FindObjectOfType<PlayerController>();
        damageBurst = Resources.Load<GameObject>("Prefabs/Utilities/DamageBurst");
        damageNum = Resources.Load<GameObject>("Prefabs/Utilities/DamageNumbers");
        buffs = new Queue<Buff>();
        //strength.baseValue = 10;
        //intelligence.baseValue = 10;
        //SetStats();
    }

    void Update() {
        Activity();
    }

    public void TakeDamage(float damage) {
        damage -= armor.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        if (this != null) {
            currentHealth -= damage;
            Instantiate(damageBurst, transform.position, transform.rotation);
            var clone = (GameObject)Instantiate(damageNum, transform.position, Quaternion.identity);
            clone.GetComponent<FloatingNumbers>().damageNumber = damage;
        }
        if (currentHealth <= 0) {
            Die();
        }
    }

    protected virtual void Activity() {
        if (currentHealth <= 0) Die();
        Regenerate();

    }

    protected virtual void Die() {
        //die in some way
        Debug.LogWarning("On " + gameObject.name + " attached CharacterStat component");
    }

    protected void Regenerate() {
        if (currentMana < maxMana) {
            currentMana += PlayerStats.instance.intelligence.GetValue() / 10 * Time.deltaTime;
        }
    }

    public void UseMana(float mana) {
        currentMana -= mana;
    }

    public bool AddBuff(Buff buff) {
        if (buff == null)
            return false;
        else if (!buffs.Contains(buff)) {
            buffs.Enqueue(buff);
            StartCoroutine(Buff());
            return true;
        }
        else return false;
    }

    IEnumerator Buff() {
        Buff buff = buffs.Dequeue();

        if (buff.buffType == global::Buff.BuffType.Damage) {
            damage.baseValue *= buff.value;
            yield return new WaitForSeconds(buff.time);
            damage.baseValue /= buff.value;
        }
        else if (buff.buffType == global::Buff.BuffType.Armor) {
            armor.baseValue *= buff.value;
            yield return new WaitForSeconds(buff.time);
            armor.baseValue /= buff.value;
        }
        else if (buff.buffType == global::Buff.BuffType.Health) {
            float time = 0;
            while (time < buff.time) {
                yield return new WaitForSeconds(1f);
                time += 1;
                TakeDamage(buff.value);
            }
        }
        else if (buff.buffType == global::Buff.BuffType.Movement) {
            bool isPlayer = GetComponent<PlayerStats>();

            if (isPlayer) PlayerController.main.moveSpeed *= buff.value;

            else {
                EnemyController enemy = GetComponent<EnemyController>();
                enemy.MoveSpeed *= buff.value;
            }
            yield return new WaitForSeconds(buff.time);

            if(isPlayer) PlayerController.main.moveSpeed /= buff.value;

            else {
                EnemyController enemy = GetComponent<EnemyController>();
                enemy.MoveSpeed /= buff.value;
            }
        }
        else if(buff.buffType == global::Buff.BuffType.Stun) {
            bool isPlayer = GetComponent<PlayerStats>();

            //if(isPlayer) 
        }
        yield return null;
    }
    
    public void SetExperience(float exp) {
        experience += exp;
        float experienceNeeded = GameLogic.ExperienceForNextLevel(Level);
        //Level up
        if (experience >= experienceNeeded) LevelUp();
    }
    
    public void SetStats() {
        SetExperience(0);
        maxHealth = GameLogic.CalculateHealth(this);
        maxMana = GameLogic.CalculateMana(this);
        currentHealth = maxHealth;
        currentMana = maxMana;
    }

    void UpdateStats() {
        float perHealth = currentHealth / maxHealth;
        float perMana = currentMana / maxMana;
        maxHealth = GameLogic.CalculateHealth(this);
        maxMana = GameLogic.CalculateMana(this);
        currentHealth = perHealth * maxHealth;
        currentMana = perMana * maxMana;
    }

    void LevelUp() {
        level++;
        strength.baseValue++;
        intelligence.baseValue++;
        UpdateStats();
    }

    public int Level { get { return level; } }
    public float Experience { get { return experience; } }

}
