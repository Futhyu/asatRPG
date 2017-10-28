using UnityEngine;

public class EnemiesDatabase : MonoBehaviour{

    #region Singleton
    public static EnemiesDatabase instance;

    void Awake() {
        if (instance == null) instance = this;
    }
    #endregion
    

}
