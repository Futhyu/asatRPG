using UnityEngine;
using UnityEngine.UI;

public class AbilityBookButton : MonoBehaviour {

    public Ability ability;
    private Button button;

	void Start () {
        if (button == null) button = GetComponentInParent<Button>();
        button.onClick.AddListener(() => {

            //if (!AbilityManager.instance.abilityList.Contains(AbilityManager.GetAbility(ability))) {
            //    AbilityManager.instance.AbilityToHotbar(AbilityManager.GetAbility(ability));
            //    AbilityManager.instance.abilityList.Add(AbilityManager.GetAbility(ability));
            //}
            AbilityManager.instance.AbilityToHotbar(ability);

        });
	}
	
	
	void Update () {
	    
	}
}
