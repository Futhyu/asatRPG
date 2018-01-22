using UnityEngine;
using UnityEngine.UI;

public class ChestSlot : MonoBehaviour {

    public Image icon;

    private Item item;

    public void AddItem(Item newItem) {
        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;
        icon.SetNativeSize();
    }

    public void ClearSlot() {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
    }
    
    public void UseItem() {
        if (item != null) {
            bool wasGrabbed = ChestUI.instance.chest.Grab(item);
            if(wasGrabbed) ClearSlot();
        }
    }
    public void OnRemoveButton() {
        ChestUI.ToogleChest();
    }
}
