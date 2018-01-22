using UnityEngine;

public class NPCTalk : Interactable {
    
    #region Singleton
    public static NPCTalk instance;

    void Awake() {
        if (instance == null) instance = this;
    }
    #endregion
    [Tooltip("Just add  a npc parent of object, okay?")]
    public NPCController NPC;

    void Start() {
        textToDisplay = "Press E to talk with " + NPC.name;
    }

    public override void Interact() {
        base.Interact();
        NPC.NPCTalk();
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
