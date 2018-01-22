using UnityEngine;

public class ChestUI : MonoBehaviour {

    #region Singleton
    public static ChestUI instance;

    void Awake() {
        if (instance == null) instance = this;
    }
    #endregion

    public Chest chest;
    public GameObject chestUI;
    public Transform items;

    private ChestSlot[] slots;
    
    void Start() {
        slots = items.GetComponentsInChildren<ChestSlot>();
    }

    void UpdateUI() {
        for (int i = 0; i < slots.Length; i++) {
            if (i < chest.content.Count) {
                slots[i].AddItem(chest.content[i]);
            }
            else {
                slots[i].ClearSlot();
            }
        }
    }

    public static void ToogleChest() {
        instance.chestUI.SetActive(!instance.chestUI.activeSelf);
        instance.UpdateUI();
    }

    
}
