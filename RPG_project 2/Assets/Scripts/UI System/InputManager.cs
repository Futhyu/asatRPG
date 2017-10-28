using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {
    
    public delegate void InputEvent();

    public static event InputEvent OnPressUp;
    public static event InputEvent OnPressDown;
    public static event InputEvent KeyPressDown;
    public static event InputEvent OnScrollUp;
    public static event InputEvent OnScrollDown;
    public static event InputEvent OnAlpha;
    
	void Update () {

        if (Input.GetMouseButtonUp(0)) {
            if (OnPressUp != null)
                OnPressUp();
        }
        if (Input.GetMouseButtonDown(0)) {
            if (OnPressDown != null)
                OnPressDown();
        }
        if (Input.anyKeyDown) {
            if (KeyPressDown != null)
                KeyPressDown();
            Debug.Log("Input key: " + Input.inputString);
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.I)) {
            InventoryUI.ToogleInventory();
        }
        //if (Input.GetKeyDown(KeyCode.E)) {
        //    if (NPCTalk.instance.canTalk) {
        //        UIManager.instance.screenText.gameObject.SetActive(false);
        //        NPCController.instance.ShowQuestInfo();
        //    }
        //}

        if (Input.GetKeyDown(KeyCode.B)) {
            QuestManager.instance.ToogleQuestBook(!UIManager.instance.questBook.gameObject.activeInHierarchy);
        }

        if(Input.mouseScrollDelta.y <= -1) {
            if(OnScrollDown != null)
                OnScrollDown();
        }

        if(Input.mouseScrollDelta.y >= 1) {
            if(OnScrollUp != null)
                OnScrollUp();
        }

        if(Input.GetKeyDown(KeyCode.Alpha1) ||
           Input.GetKeyDown(KeyCode.Alpha2) ||
           Input.GetKeyDown(KeyCode.Alpha3) ||
           Input.GetKeyDown(KeyCode.Alpha4) ||
           Input.GetKeyDown(KeyCode.Alpha5) ||
           Input.GetKeyDown(KeyCode.Alpha6) ||
           Input.GetKeyDown(KeyCode.Alpha7) ||
           Input.GetKeyDown(KeyCode.Alpha8) ||
           Input.GetKeyDown(KeyCode.Alpha9) ||
           Input.GetKeyDown(KeyCode.Alpha0)) {
            if(OnAlpha != null)
                OnAlpha();

        }

        if (Input.GetKeyDown(KeyCode.F)) {
            if (OnPressDown != null) {
                OnPressDown();
            }
        }

        if (Input.GetKeyDown(KeyCode.N)) {
            AbilityManager.instance.ToogleAbilityBook(!UIManager.instance.abilityBook.gameObject.activeInHierarchy);
        }
        
    }
}
