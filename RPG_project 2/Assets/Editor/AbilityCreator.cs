using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
using System.Text;

public class AbilityCreator : EditorWindow {

	[MenuItem("Ability Maker/Ability Wizard")]
    static void Init() {
        AbilityCreator abilityWindow = (AbilityCreator)CreateInstance(typeof(AbilityCreator));
        abilityWindow.Show();
    }

    Ability tempAbility = null;
    AbilityManager abilityManager = null;

    void OnGUI() {

        if (abilityManager == null) abilityManager = FindObjectOfType<AbilityManager>();
        
        if (GUILayout.Button("Create New Ability")) {
            if (tempAbility == null) tempAbility = new Ability();
        }
        if (tempAbility != null) {
            tempAbility.abilityName = EditorGUILayout.TextField("Ability Name", tempAbility.abilityName);
            tempAbility.icon = (Sprite)EditorGUILayout.ObjectField("Ability Icon", tempAbility.icon, typeof(Sprite), false);
            tempAbility.manaCost = EditorGUILayout.IntField("Mana Cost", tempAbility.manaCost);
            tempAbility.projectileSpeed = EditorGUILayout.FloatField("Projectile Speed", tempAbility.projectileSpeed);
            tempAbility.cooldown = EditorGUILayout.FloatField("Cooldown", tempAbility.cooldown);
            tempAbility.castTime = EditorGUILayout.FloatField("Cast Time", tempAbility.castTime);
        }
        EditorGUILayout.Space();

        if (tempAbility == null) {
            //if ()) {

            //    //tempAbility = new Ability();

            //}
        }
        else {
            if (GUILayout.Button("Add to database")) {
                
                string json = File.ReadAllText(Application.dataPath + "/Resources/Json/AbilityDatabase.json");
                StringBuilder temp = new StringBuilder(json);
                string obj = JsonUtility.ToJson(tempAbility);
                //string text = File.ReadAllLines(Application.dataPath + "/Resources/Json/AbilityDatabase.json");
                //json += obj;
                //File.AppendAllText(Application.dataPath + "/Resources/Json/AbilityDatabase.json", obj);
                temp.Remove(temp.Length - 1, 1);
                temp.Append("," + obj + "]");
                //Debug.Log(json);

                //json.Insert(json.LastIndexOf(','), "asdasd");
                //Debug.Log(json);


                using (StreamWriter sw = new StreamWriter(Application.dataPath + "/Resources/Json/AbilityDatabase.json")) {

                    sw.WriteLine(temp.ToString());
                    
                }
                tempAbility = null;
            }


            if (GUILayout.Button("Reset")) {
                Reset();
            }
        }

    }

    void Reset() {
        //tempAbility.abilityName = "";
        //tempAbility.icon = null;
        //tempAbility.manaCost = 0;
        //tempAbility.minDamage = 0;
        //tempAbility.manaCost = 0;
        //tempAbility.maxDamage = 0;
        //tempAbility.spellPrefab = null;
        //tempAbility.projectileSpeed = 0;
    }

}
