using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler {

    [HideInInspector]
    public bool isEmpty = true;
    [HideInInspector]
    public bool isEquipSlot;
    public int id;
    
    public void OnDrop(PointerEventData eventData) {

        GameObject droppedObj = eventData.pointerDrag.gameObject;

        if (Input.GetKey(KeyCode.LeftShift)) {
            Spell droppedAbility = droppedObj.GetComponent<Spell>();
            if (isEmpty) {

                droppedAbility.transform.SetParent(this.transform);
                droppedAbility.transform.position = this.transform.position;
               // inv.items[id] = droppeditem;
                HotbarController.instance.slots[droppedAbility.slot].isEmpty = true;
                droppedAbility.slot = id;
                isEmpty = false;

            }
            else {

                Transform oldAbility = transform.GetChild(1);
                //Debug.Log(oldAbility.name);
                ////swap
                //item.GetComponent<BaseItem>().slot = droppedAbility.slot;
                //item.transform.SetParent(inv.invSlots[droppedAbility.slot].transform);
                //item.transform.position = inv.invSlots[droppedAbility.slot].transform.position;
                oldAbility.transform.SetParent(HotbarController.instance.slots[droppedAbility.slot].transform);
                oldAbility.transform.position = HotbarController.instance.slots[droppedAbility.slot].transform.position;
                oldAbility.GetComponent<Spell>().slot = droppedAbility.slot;
                droppedAbility.slot = id;
                droppedAbility.transform.SetParent(transform);
                droppedAbility.transform.position = transform.position;
            }
        }
    }
    
    void Update() {
        //if (GetComponentInChildren<Spell>()) isEmpty = false;
        
        //else {
        //    isEmpty = true;
        //}
    }

}
