using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class HotbarSlot : MonoBehaviour {

    public Transform original;
    public Transform hotbarSlot;
    public Transform thisSlot;
    public Transform[] Items;
    public GameObject objToInst;
    public int scrollPosition;

	void Start () {
	
	}
	
	void Update () {
        if (Input.mouseScrollDelta.y > 0 || Input.mouseScrollDelta.y < 0) {
            InputManager.OnScrollUp += ScrollUp;
            InputManager.OnScrollDown += ScrollDown;
        }
        else {
            InputManager.OnScrollUp -= ScrollUp;
            InputManager.OnScrollDown -= ScrollDown;
            Selected();
        }
    }

    void ScrollUp() {
        scrollPosition++;
        if(scrollPosition > 10) {
            scrollPosition = 1;
        }
    }
    void ScrollDown() {
        scrollPosition--;
        if(scrollPosition < 1) {
            scrollPosition = 10;
        }
    }
    void Selected() {
        if (thisSlot.name =="HBSlot (" + scrollPosition + ")") thisSlot.GetComponent<Image>().color = Color.red;
        else thisSlot.GetComponent<Image>().color = original.GetComponent<Image>().color;
    }
}
