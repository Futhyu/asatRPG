using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class AbilityManager : MonoBehaviour {

    public static AbilityManager instance;
    
    //public delegate void OnAbilityCast(Ability ability);
    //public OnAbilityCast onAbilityCast;
    //public List<Ability> abilityList = new List<Ability>();
    //public List<Ability> database = new List<Ability>();
    
    private HotbarController hb;

    private bool isCast;

    void Awake() {
        if (instance == null) instance = this;
        hb = FindObjectOfType<HotbarController>();
    }
    
    #region Ability
    void AddAbility(Ability ability) {

        GameObject abilityObj = Instantiate(Resources.Load<GameObject>("Prefabs/Ability"), hb.slots[FindEmptySlot()].transform) as GameObject;
        
        abilityObj.GetComponent<Image>().sprite = ability.icon;
        
        abilityObj.GetComponent<Spell>().abilitySpell = ability;
        abilityObj.transform.localScale = Vector3.one;
        abilityObj.transform.localPosition = Vector3.zero;

    }

    public void CastAbility(Spell ability, Transform magician) {

        if (ability == null) {
            Debug.LogWarning("Spell is null! Must assign it!");
            return;
        }
        else if (ability.isCooldown) {
            Debug.Log(ability.abilitySpell.abilityName + " is on cooldown. Wait");
        }
        else if (!isCast) {
            if (magician.GetComponent<CharacterStats>().currentMana >= ability.abilitySpell.manaCost)
                magician.GetComponent<CharacterStats>().UseMana(ability.abilitySpell.manaCost);

            if (ability.abilitySpell.type == Ability.AbilityType.Range) {
                GameObject abilityObj = (GameObject)Instantiate(ability.abilitySpell.spellPrefab, magician.position, Camera.main.GetComponent<Transform>().rotation, magician);
                //    abilityObj.GetComponent<Animator>().SetBool("isFireball", ability.abilityName == "Fireball");

                Vector3 pos = Camera.main.WorldToScreenPoint(PlayerController.main.transform.position);
                Vector2 dir = Input.mousePosition - pos;
                var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                abilityObj.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                abilityObj.GetComponent<Rigidbody2D>().gravityScale = 0;
                abilityObj.GetComponent<Rigidbody2D>().velocity = dir.normalized * ability.abilitySpell.projectileSpeed;

            }
            if (ability.abilitySpell.type == Ability.AbilityType.AOE) {
                Instantiate(ability.abilitySpell.spellPrefab, magician.position, Camera.main.GetComponent<Transform>().rotation, magician);
                //abilityObj.transform.position = magicSpawn.position;
                //abilityObj.transform.SetParent(transform.parent);
            }
            
        }
    }
    #endregion

    private int FindEmptySlot() {
        for (int i = 0; i < hb.slots.Length; i++) {
            if (hb.slots[i].GetComponent<Slot>().isEmpty) {
                hb.slots[i].GetComponent<Slot>().isEmpty = false;
                return i;
            }
        }
        return 26;
    }

    public void ToogleAbilityBook(bool b) {
        UIManager.instance.abilityBook.gameObject.SetActive(b);
    }

    
    public void AbilityToHotbar(Ability ability) {
        AddAbility(ability);
    }
    
    
    private IEnumerator Casting(float castTime) {
        yield return new WaitForSeconds(castTime);
        
    }
    
    public bool IsCast { get { return isCast; } set { isCast = value; } }
}

