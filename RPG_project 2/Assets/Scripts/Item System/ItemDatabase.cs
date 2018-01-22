#if UNITY_EDITOR
using UnityEditor;

public class ItemDatabase {

    #region Singleton
    public static ItemDatabase instance;
    
    void Awake() {
        if (instance == null) instance = this;
    }
    #endregion

    public Item[] database;

    public int LastID() {
        RefreshDatabase();
        return database.Length;
    }

    void RefreshDatabase() {
        database = AssetDatabase.LoadAllAssetsAtPath("Assets/Database/Items/") as Item[];
    }
}
#endif