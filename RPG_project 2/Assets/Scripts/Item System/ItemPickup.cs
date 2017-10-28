using UnityEngine;

public class ItemPickup : Interactable {

    public Item item;

    public override void Interact() {
        base.Interact();
        FindObjectOfType<Inventory>();
        PickUp();
    }

    void PickUp() {
        Debug.Log("Pick up " + item.name);
        bool wasPickedUp = Inventory.instance.Add(item);
        if(wasPickedUp) Destroy(gameObject);
    }

}
