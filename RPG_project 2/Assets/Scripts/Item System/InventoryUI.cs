using UnityEngine;

public class InventoryUI : MonoBehaviour {

    #region Singleton
    public static InventoryUI instance;
    void Awake() {
        if (instance == null) instance = this;
    }
    #endregion

    private Inventory inventory;
    public GameObject inventoryUI;
    public Transform items;

    private InventorySlot[] slots;

    void Start () {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;

        slots = items.GetComponentsInChildren<InventorySlot>();
	}
	
	void Update () {
        //if (Input.GetButtonDown("Inventory")) {
        //    inventoryUI.SetActive(!inventoryUI.activeSelf);
        //}
	}

    void UpdateUI() {
        for(int i = 0; i < slots.Length; i++) {
            if(i < inventory.items.Count) {
                slots[i].AddItem(inventory.items[i]);
            }else {
                slots[i].ClearSlot();
            }
        }
    }

    public static void ToogleInventory() {
        instance.inventoryUI.SetActive(!instance.inventoryUI.activeSelf);
    }

}
