using UnityEngine;
using System.Collections.Generic;

public static class PlayerData {

    public static List<Quest> activeQuests = new List<Quest>();
    public static List<Quest> finishedQuests = new List<Quest>();
    public static List<Quest.QuestKill> monsterKilled = new List<Quest.QuestKill>();

    public static List<Item> itemFound = new List<Item>();

    /// <summary>
    /// Adds the quest to "activeQuests" List
    /// </summary>
    /// <param name="id">Identifier.</param>
    
    
    public static void Refresh(Quest quest) {
        activeQuests.Remove(quest);
        finishedQuests.Add(quest);
        Quest.QuestKill[] kills = quest.task.kills;
        //int[] keys = new int[monsterKilled.Keys.Count];
        //monsterKilled.Keys.CopyTo(keys, 0);

        //for (int i = 0; i < QuestManager.instance.database[quest].task.kills.Length; i++) {
        //    kills.Add(QuestManager.instance.database[quest].task.kills[i].id);
        //}
        foreach(Quest.QuestKill kill in kills) {
            if (monsterKilled.Contains(kill)) monsterKilled.Remove(kill);
        }
    }

    public static void AddQuest(Quest quest) {
        if (activeQuests.Contains(quest))
            return;

        Quest newActiveQuest = quest;
        //newActiveQuest.dateTaken =

        if(quest.task.kills.Length > 0) {
            
            foreach(Quest.QuestKill questKill in quest.task.kills) {
                if (!monsterKilled.Contains(questKill))
                   monsterKilled.Add(questKill);
            }
        }

        if(quest.task.items.Length > 0) {

            //newActiveQuest.items = new Quest.QuestItem[quest.task.items.Length];

            foreach(Quest.QuestItem questItem in quest.task.items) {

                //for (int i = 0; i < newActiveQuest.items.Length; i++) {
                //    if(newActiveQuest.items[i] == null)
                //        newActiveQuest.items[i] = new Quest.QuestItem(questItem.id, questItem.amount);
                    
                //}
                
                
                if (!itemFound.Contains(questItem.item))
                    itemFound.Add(questItem.item);

                //for (int i = 0; i < newActiveQuest.items.Length; i++) {

                //    newActiveQuest.items[i].amount = itemFound[questItem.id].amount;

                //}
                
                QuestManager.instance.SpawnItem(questItem.item, new Vector2(4f,-5f));
            }

        }

        activeQuests.Add(newActiveQuest);
    }
    
}
