using UnityEngine;
using System.Collections;

public class NPCTalk : Interactable {
    
    #region Singleton
    public static NPCTalk instance;

    void Awake() {
        if (instance == null) instance = this;
    }
    #endregion
    [Tooltip("Just add npc parent of object, okay?")]
    public NPCController npc;

    public override void Interact() {
        base.Interact();
        npc.NPCTalk();
    }

    //[HideInInspector]
    //public bool canTalk;

    //void OnTriggerEnter2D(Collider2D other) {
    //    if (other.gameObject.tag == "Player") {
    //        UIManager.instance.screenText.gameObject.SetActive(true);
    //        canTalk = true;
    //    }
    //}

    //void OnTriggerExit2D(Collider2D other) {
    //    if (other.gameObject.tag == "Player") {
    //        UIManager.instance.screenText.gameObject.SetActive(false);
    //        canTalk = false;
    //    }
    //}

}
