using System.Collections.Generic;

public class Chest : Interactable {

    private Inventory inventory;

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;
    
    public List<Item> content = new List<Item>();
    public bool isLocked = false;
    public Item key = null;
    public int space = 6;
    
    void Start() {
        inventory = Inventory.instance;
        textToDisplay = "Press E to open a chest";
    }

    public override void Interact() {
        base.Interact();
        if (OpenChest()) {
            ChestUI.instance.chest = this;
            ChestUI.ToogleChest();
            if (onItemChangedCallback != null) onItemChangedCallback.Invoke();
        }
    }

    public bool Grab(Item item) {
        if (!Inventory.IsFull()) {
            content.Remove(item);
            if (onItemChangedCallback != null) onItemChangedCallback.Invoke();
            inventory.Add(item);
            return true;
        }
        return false;
    }

    bool OpenChest() {
        if(!isLocked) return true;

        if(Inventory.instance.HasItem(key)) {
            Inventory.instance.Remove(key);
            isLocked = false;
            return true;
        }
        return false;
    }

}
