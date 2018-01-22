using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    #region Singleton

    public static Inventory instance;

    void Awake() {
        if(instance == null) instance = this;
    }
    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public int space = 20;

    public List<Item> items = new List<Item>();

    public bool Add(Item item) {
        if (item.type != ItemType.Default) {
            if(IsFull()) {
                return false;
            }
            items.Add(item);

            if(onItemChangedCallback != null) onItemChangedCallback.Invoke();
        }
        return true;
    }

    public void Remove(Item item) {
        items.Remove(item);
        if (onItemChangedCallback != null) onItemChangedCallback.Invoke();
    }

    public void Drop(Item item) {
        items.Remove(item);
        QuestManager.instance.SpawnItem(item, new Vector2(transform.position.x, transform.position.y-1));
        if (onItemChangedCallback != null) onItemChangedCallback.Invoke();
    }

    public static bool IsFull() {
        return instance.items.Count >= instance.space ? true : false; 
    }

    public bool HasItem(Item item) {
        for(int i = 0; i < items.Count; i++) {
            if (items[i] == item) return true;
        }
        return false;
    }

    public Item FindItem(Item item) {
        for (int i = 0; i < items.Count; i++) {
            if (items[i] == item) return items[i];
        }
        return null;
    }

}
