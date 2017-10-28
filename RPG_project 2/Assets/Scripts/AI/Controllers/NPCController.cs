using UnityEngine;

public class NPCController : MonoBehaviour {
    
    [SerializeField]
    private Quest[] quests;
    
    private QuestManager theQM;
    private PlayerController thePlayer;

    private Quest quest;

    #region Singleton
    public static NPCController instance;
    void Awake() {
        if (instance == null) instance = this;
        
        theQM = FindObjectOfType<QuestManager>();
        thePlayer = FindObjectOfType<PlayerController>();
    }
    #endregion Singleton
    
    public void NPCTalk() {
        if (quests.Length > 0 && HasUnfinishedQuests()) {
            ShowQuestInfo();
        }
        else {
            GetComponent<DialogueTrigger>().TriggerDialogue();
        }
    }

    private void ShowQuestInfo() {
        thePlayer.canMove = false;

        for (int i = 0; i < quests.Length; i++) {
            if (!PlayerData.finishedQuests.Contains(quests[i]) && theQM.IsQuestAvailable(quests[i], PlayerStats.instance)) {

                theQM.ShowQuestInfo(quests[i]);

                UIManager.instance.questInfoAcceptButton.gameObject.SetActive(!PlayerData.activeQuests.Contains(quests[i]));

                if (theQM.IsQuestFinished(quests[i])) {
                    UIManager.instance.questInfoCompleteButton.gameObject.SetActive(true);
                    UIManager.instance.questInfoCompleteButton.onClick.AddListener(() => {
                        thePlayer.canMove = true;
                        ReceiveCompletedQuest(quests[i]);
                        PlayerData.Refresh(quests[i]);
                        //PlayerData.activeQuests.Remove(quests[i]);
                        //PlayerData.finishedQuests.Add(quests[i]);
                        UIManager.instance.questInfoCompleteButton.onClick.RemoveAllListeners();
                        UIManager.instance.questInfoCompleteButton.gameObject.SetActive(false);
                        UIManager.instance.questInfo.gameObject.SetActive(false);

                        //deleting quest items from inventory
                        foreach (var questItem in quests[i].task.items) {
                            Inventory.instance.Remove(questItem.item);
                        }
                        //destroy quest from questbook
                        Destroy(UIManager.instance.questBookContent.Find(quests[i].id.ToString()).gameObject);
                    });
                }


                break;
            }
        }
        

        //foreach (int i in quests) {
        //    if (!PlayerData.finishedQuests.Contains(i) && theQM.IsQuestAvailable(i, thePlayer)) {
        //        theQM.ShowQuestInfo(theQM.database[i]);
        //        UIManager2.instance.questInfoAcceptButton.gameObject.SetActive(!PlayerData.activeQuests.ContainsKey(i));
        //        if (theQM.IsQuestFinished(i)) {
        //            UIManager2.instance.questInfoCompleteButton.gameObject.SetActive(true);
        //            UIManager2.instance.questInfoCompleteButton.onClick.AddListener(() => {
        //                thePlayer.canMove = true;
        //                ReceiveCompletedQuest(theQM.database[quests[i]]);
        //                PlayerData.activeQuests.Remove(i);
        //                PlayerData.finishedQuests.Add(i);
        //                UIManager2.instance.questInfoCompleteButton.onClick.RemoveAllListeners();
        //                UIManager2.instance.questInfoCompleteButton.gameObject.SetActive(false);
        //                UIManager2.instance.questInfo.gameObject.SetActive(false);
        //            });
        //        }
        //        break;
        //    }
        //    else if (i < quests.Length) {
        //        Debug.Log(0);
        //        continue;
        //    }
        //}
    }

    //public void ShowDialogue() {
    //    thePlayer.canMove = false;
    //    if (dialogueIndex > (dialogues.Length - 1)) {
    //        theDM.CloseDialogueBox();
    //        thePlayer.canMove = true;
    //        dialogueIndex = dialogues.Length - 2;

    //    }
    //    else {
    //        theDM.PrintOnDialogueBox(dialogues[dialogueIndex]);
    //    }
    //    dialogueIndex++;

    //}

    bool HasUnfinishedQuests() {
        bool temp = true;
        foreach(Quest quest in quests) {
            if (temp) temp = IsQuestUnfinished(quest);
            else break;
        }
        return temp;
    }

    bool IsQuestUnfinished(Quest quest) {
        if (!PlayerData.finishedQuests.Contains(quest)) {
            return true;
        }
        else return false;
    }

    void ReceiveCompletedQuest(Quest quest) {
        if (quest.reward.exp > 0) {
            PlayerStats.instance.SetExperience(GameLogic.CalculateExpForQuest(quest));
        }
        if(quest.reward.items.Length > 0) {
            foreach(Quest.QuestItem item in quest.reward.items) {
                thePlayer.GetComponentInParent<Inventory>().Add(item.item);
                Debug.Log("You got: " + item.amount +" "+  item.item.name);
                //ex. inventory.add(item.id< item.amount);
            }
        }
        
    }

}
