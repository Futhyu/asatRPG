﻿using UnityEngine;

public class EquipmentManager : MonoBehaviour {

    #region Singleton
    public static EquipmentManager instance;

    void Awake() {
        if (instance == null) instance = this;
    }
    #endregion

    Equipment[] currentEquipment;

    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;

    Inventory inventory;

    void Start() {
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];
        inventory = Inventory.instance;
    }

    public void Equip(Equipment newItem) {
        int slotIndex = (int)newItem.equipSlot;

        Equipment oldItem = null;

        if(currentEquipment[slotIndex] != null) {
            oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);
        }
        
        if(onEquipmentChanged != null) {
            onEquipmentChanged.Invoke(newItem, oldItem);
        }

        currentEquipment[slotIndex] = newItem;

        PlayerController.main.Equip(newItem);
    }

    public void Unequip(int slotIndex) {
        if (currentEquipment[slotIndex] != null) {
            Equipment oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);

            currentEquipment[slotIndex] = null;
            
            PlayerController.main.Unequip(oldItem);
            
            if (onEquipmentChanged != null) {
                onEquipmentChanged.Invoke(null, oldItem);
            }
        }
    }

    public void UnequipAll() {
        for(int i = 0; i < currentEquipment.Length; i++) {
            Unequip(i);
        }
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.U)) UnequipAll();
    }

    public static float GetDamage() {
        float damage = 0;
        foreach (Equipment item in instance.currentEquipment) {
            if (item != null) {
                damage += item.damageModifier;
            }
        }
        return damage;
    }
    
}
