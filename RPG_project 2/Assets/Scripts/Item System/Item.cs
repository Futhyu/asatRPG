using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject {

    public int id = 0;
    new public string name = "New Item";
    public Sprite icon = null;
    public bool isDefaultItem = false;
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
