using UnityEngine;
using System;

public class HotbarController : MonoBehaviour {
    #region Singleton
    public static HotbarController instance;

    void Awake() {
        if (instance == null) instance = this;
    }
    #endregion

    public Transform original;
    public Slot previosSlot;
    public Slot thisSlot;

    public Transform[] Items;
    public GameObject objToInst;
    public int scrollPosition;
    public Slot[] slots;

    public GameObject draggedItem;
    Transform draggedItemParent;

    public Transform inventory;

    void Start() {
        slots = transform.GetChild(0).gameObject.GetComponentsInChildren<Slot>();
        thisSlot = slots[0];
        previosSlot = slots[0];
        //Selected();
       // Debug.Log(slots.Length);
        for(int i = 0; i < slots.Length; i++) {
            slots[i].GetComponentInParent<Slot>().id = i;
        }
    }

    void Update() {



        //if (Input.mouseScrollDelta.y > 0 || Input.mouseScrollDelta.y < 0) {
        //    InputManager.OnScrollUp += ScrollUp;
        //    InputManager.OnScrollDown += ScrollDown;
            
        //}
        //else {
        //    InputManager.OnScrollUp -= ScrollUp;
        //    InputManager.OnScrollDown -= ScrollDown;
            
        //}

        if (Input.anyKeyDown)
            InputManager.OnAlpha += Num;
        else
            InputManager.OnAlpha -= Num;
        
    }

    //void ScrollUp() {
    //    scrollPosition++;
        
    //    if (scrollPosition > 9) {
    //        scrollPosition = 0;
    //    }

    //    previosSlot = thisSlot;
    //    thisSlot = slots[scrollPosition];
    //    Selected();
    //}
    //void ScrollDown() {
    //    scrollPosition--;
        
    //    if (scrollPosition < 0) {
    //        scrollPosition = 9;
    //    }
    //    previosSlot = thisSlot;
    //    thisSlot = slots[scrollPosition];
    //    Selected();
    //}

    void Num() {
        string str = Input.inputString;
        scrollPosition = Convert.ToInt32(str);
        if (scrollPosition == 0) {
            scrollPosition = 10;
        }
        if (thisSlot == slots[scrollPosition - 1]) {
            thisSlot = slots[scrollPosition - 1];
            
        }
        else {
            previosSlot = thisSlot;
            thisSlot = slots[scrollPosition - 1];
            //Selected();
        }
        if(!thisSlot.isEmpty) {
            thisSlot.GetComponentInChildren<Spell>().CastSpell();
        }
    }

    //void Selected() {
    //    thisSlot.GetComponent<Image>().color = Color.red;
    //    previosSlot.GetComponent<Image>().color = original.GetComponent<Image>().color;
    //}
    
}
