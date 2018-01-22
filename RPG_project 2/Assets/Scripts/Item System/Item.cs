using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject {

    public int id = 0;
    new public string name = "New Item";
    public Sprite icon = null;

    public ItemType type;

    [Space(10)]
    [Tooltip("For animating item. If it has not then null here")]
    public RuntimeAnimatorController controller;
    [Space(10)]
    [Range(1, 64)]
    public int quantity = 1;

    public virtual void Use() {
        //use the item
        Debug.Log("Using " + name);
    }

    public void RemoveFromInventory() {
        Inventory.instance.Remove(this);
    }
}

public enum ItemType { Default, Quest, Equipment, Key, Potion }