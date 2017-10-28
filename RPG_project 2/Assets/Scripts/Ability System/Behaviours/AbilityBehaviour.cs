using UnityEngine;

[System.Serializable]
public class AbilityBehaviour : MonoBehaviour{

    private string abName;
    private string abDescription;
    private BehaviourStartTimes startTime;
    
    public enum BehaviourStartTimes { Beginning, Middle, End}

    public virtual void PerformBehaviour(GameObject player, GameObject abilityPrefab) {

        Debug.LogWarning("Need to add Behaviour!");

    }

    public string Name { get { return abName; } set { abName = value; } }
    public string Description { get { return abDescription; } set { abDescription = value; } }
    public BehaviourStartTimes StartTime { get { return startTime; } set { startTime = value; } }

}
