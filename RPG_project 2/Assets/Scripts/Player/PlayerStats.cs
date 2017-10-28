using UnityEngine;

public class PlayerStats : CharacterStats {
    
    #region Singleton
    public static PlayerStats instance;

    void Awake() {
        if (instance == null) instance = this;

    }
    #endregion
    
    void Start () {
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
        SetStats();
        damageBurst = Resources.Load<GameObject>("Prefabs/Utilities/DamageBurst");
        damageNum = Resources.Load<GameObject>("Prefabs/Utilities/DamageNumbers");
        buffs = new System.Collections.Generic.Queue<Buff>();
    }
	
	void Update () {
        Activity();
	}

    protected override void Die() {
        Destroy(gameObject);
    }

    void OnEquipmentChanged(Equipment newItem, Equipment oldItem) {
        if (newItem != null) {
            armor.AddModifier(newItem.armorModifier);
            damage.AddModifier(newItem.damageModifier);
        }

        if (oldItem != null) {
            armor.RemoveModifier(oldItem.armorModifier);
            damage.RemoveModifier(oldItem.damageModifier);
        }
    }

}
