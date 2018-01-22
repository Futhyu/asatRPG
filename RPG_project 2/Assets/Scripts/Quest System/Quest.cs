using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New Quest", menuName = "Quest")]
public class Quest : ScriptableObject{

    public int id;
    public string questName;
    [TextArea]
    public string description;
    public int reciepent;
    public int requiredLevel;
    public Reward reward;
    public Task task;

    public Quest(int id, string questName, string description, int reciepent, int requiredLevel, Reward reward, Task task) {
        this.id = id;
        this.questName = questName;
        this.description = description;
        this.reciepent = reciepent;
        this.requiredLevel = requiredLevel;
        this.reward = reward;
        this.task = task;
    }

    [Serializable]
    public class Reward {
        public float exp;
        public float money;
        public QuestItem[] items;

        public Reward(float exp, float money, QuestItem[] items) {
            this.exp = exp;
            this.money = money;
            this.items = items;
        }
    }
    
    [Serializable]
    public class Task {
        public int talkTo;
        public QuestItem[] items;
        public QuestKill[] kills;

        public Task(int talkTo, QuestItem[] items, QuestKill[] kills) {
            this.talkTo = talkTo;
            this.items = items;
            this.kills = kills;

        }
    }

    [Serializable]
    public class QuestItem {
        public Item item;
        public int amount;
        
        public QuestItem(Item item, int amount) {
            this.item = item;
            this.amount = amount;

        }

    }

    [Serializable]
    public class QuestKill {
        public string name;
        public int amount;
        public int initialAmount;

        public QuestKill(string name, int amount, int initialAmount) {
            this.name = name;
            this.amount = amount;
            this.initialAmount = initialAmount;
        }

    }

}
