using UnityEngine;

public class ItemDatabase : MonoBehaviour{

    #region Singleton
    public static ItemDatabase instance;
    
    void Awake() {
        if (instance == null) instance = this;
    }
    #endregion
    
    void Start() {

    }

}
