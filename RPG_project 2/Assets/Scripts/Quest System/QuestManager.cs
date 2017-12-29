using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using LitJson;

public class QuestManager : MonoBehaviour {
    
    public static QuestManager instance;
    private UIManager theUI;
    private PlayerController thePlayer;
    
    private Transform canvas;
    
    void Awake() {
        theUI = FindObjectOfType<UIManager>();
        canvas = FindObjectOfType<Canvas>().transform;
        thePlayer = FindObjectOfType<PlayerController>();

        if (instance == null)
            instance = this;
    }
    
    public void ToogleQuestBook(bool b) {
        UIManager.instance.questBook.gameObject.SetActive(b);
        if (b)
            ShowActiveQuests();

    }

    public void ShowActiveQuests() {

        foreach (Quest activeQuest in PlayerData.activeQuests) {
            int i = activeQuest.id;

            if (UIManager.instance.questBookContent.Find(i.ToString()) != null)
                continue;

            GameObject QuestButtonGo = Instantiate(Resources.Load("Prefabs/Quest_Button") as GameObject);
            QuestButtonGo.name = activeQuest.id.ToString();
            QuestButtonGo.transform.SetParent(theUI.questBookContent);
            QuestButtonGo.transform.localScale = Vector3.one;
            QuestButtonGo.transform.Find("Text").GetComponent<Text>().text = activeQuest.questName;
            
            QuestButtonGo.GetComponent<Button>().onClick.AddListener(() => {
                UIManager.instance.questInfoAcceptButton.gameObject.SetActive(false);
                ShowQuestInfo(activeQuest);
            });

        }
    }

    public bool IsQuestAvailable(Quest quest, PlayerStats thePS) {     
            return (quest.requiredLevel <= thePS.Level);
    }

    public bool IsQuestFinished(Quest quest) {
        
        //check kills
        
        if (quest.task.kills.Length > 0) {
            
            foreach (Quest.QuestKill questKill in quest.task.kills) {
                //if quest already in active quests or kill is clear???
                if (!PlayerData.activeQuests.Contains(quest) || !PlayerData.monsterKilled.Contains(questKill)) {
                    return false;
                }
                //Debug.Log("Before. Count is initial is " + PlayerData.activeQuests[questId].kills[questId].initialAmount + " and questkillid is " + questKill.id);
                int currentKills = PlayerData.monsterKilled.Find(x => x == questKill).amount;
                
                if (currentKills < questKill.initialAmount) {
                    return false;
                }

            }
        }
        //check items
        if (quest.task.items.Length > 0) {
            foreach (Quest.QuestItem questItem in quest.task.items) {
                if (!PlayerData.activeQuests.Contains(quest) || !PlayerData.itemFound.Contains(questItem.item)) {
                    return false;
                }
                //Debug.Log(PlayerData.activeQuests[questId].items[questId].amount);

                int current = 0;
                //for (int i = 0; i < PlayerData.activeQuests[questId].items.Length; i++) {
                //    current = PlayerData.itemFound[questItem.id].amount - PlayerData.FindItem(questItem.id);
                //}

                foreach(Item item in Inventory.instance.items) {
                    if (item != null) {
                        if (item == questItem.item) {
                            current = item.quantity;
                            break;
                        }
                    }
                    
                }

                if (current < questItem.amount) return false;

            }
        }
        //check talks
        

        return true;
    }

    public void ShowQuestInfo(Quest quest) {
        theUI.questInfo.gameObject.SetActive(true);
        
        theUI.questInfoCompleteButton.gameObject.SetActive(false);

        theUI.questInfoAcceptButton.onClick.RemoveAllListeners();
        theUI.questInfoAcceptButton.onClick.AddListener(() => {
            thePlayer.canMove = true;

            PlayerData.AddQuest(quest);
            theUI.questInfo.gameObject.SetActive(false);
            ShowActiveQuests();

        });
        //Set Texts
        Transform info = canvas.Find("Quest Info/Background/Info/Viewport/Content").transform;
        UIManager.instance.questInfoContent.Find("Name").GetComponent<Text>().text = quest.questName;
        UIManager.instance.questInfoContent.Find("Description").GetComponent<Text>().text = quest.description;

        //task
        string taskString = "Task: \n";
        if (quest.task.kills != null) {
            foreach (Quest.QuestKill qk in quest.task.kills) {

                int currentKills = 0;
                
                if (PlayerData.activeQuests.Contains(quest))
                    currentKills = PlayerData.monsterKilled.Find(x => x.name == qk.name).amount;


                taskString += "Slay " + currentKills + "/" + qk.initialAmount + " " + qk.name + ".\n";

            }

        }

        if (quest.task.items != null) {
            foreach (Quest.QuestItem qi in quest.task.items) {
                taskString += "Bring " + qi.amount + " " + qi.item.name + ".\n";

            }
        }


        //taskString += "Talk to " + NPCDatabase.database[quest.task.talkTo].name + ".";



        info.Find("Task").GetComponent<Text>().text = taskString;

        //reward

        string rewardString = "Reward: \n";
        if (quest.reward.items != null) {
            foreach (Quest.QuestItem qi in quest.reward.items) {
                rewardString += qi.amount + " " + qi.item.name + ".\n";
            }
        }
        if (quest.reward.exp > 0)
            rewardString += quest.reward.exp + " Experience.\n";
        if (quest.reward.money > 0)
            rewardString += quest.reward.money + " Money.\n";
        info.Find("Reward").GetComponent<Text>().text = rewardString;

    }

    public void SpawnItem(Item itemToSpawn, Vector2 coord) {
        if (itemToSpawn != null) {
            Debug.Log(itemToSpawn.name + " spawn at position " + coord.ToString());
            //ItemDatabase.database[i] = itemToAdd;
            GameObject questItem = Instantiate(Resources.Load<GameObject>("Prefabs/Item"));
            questItem.gameObject.name = itemToSpawn.name;
            questItem.GetComponent<ItemPickup>().item = itemToSpawn;
            questItem.transform.position = coord;
        }
    }
}
