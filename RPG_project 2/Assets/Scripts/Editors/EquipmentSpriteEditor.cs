#if UNITY_EDITOR
using UnityEditor;

using UnityEngine;

public class EquipmentSpriteEditor : EditorWindow {

    string path;
    TextureImporter importer;

    [MenuItem("Editors/Equipment Sprite Editor")]
    public static void ShowWindow() {
        GetWindow<EquipmentSpriteEditor>("Sprite Editor");
    }

    void OnGUI() {
        GUILayout.Label("Equipment", EditorStyles.boldLabel);
        
        if(GUILayout.Button("Get Path")) {
            if (IsValidTargetForPallete()) {
                path = AssetDatabase.GetAssetPath(Selection.activeObject);
                importer = (TextureImporter)TextureImporter.GetAtPath(path);
                if (importer == null) {
                    Debug.LogError("Selected object not found in Asset Database.");
                }
            }
        }

        if (GUILayout.Button("Edit and Save")) {
            bool textureHasSpritesheet = importer != null && importer.spritesheet.Length > 0;
            if(textureHasSpritesheet) Edit();
        }
    }

    void Edit() {
        SpriteMetaData[] spritesheet = importer.spritesheet;

        for (int i = 0; i < spritesheet.Length; i++) {
            //cast
            if (i < 28) {
                if(i < 7) spritesheet[i].name = "cast_up_" + i;
                else if(i > 6 && i < 14) spritesheet[i].name = "cast_left_" + (i - 7);
                else if(i > 13 && i < 21) spritesheet[i].name = "cast_down_" + (i-14);
                else if(i > 20) spritesheet[i].name = "cast_right_" + (i-21);
            }
            //walk
            else if(i > 59 && i < 96) {
                if(i < 69) spritesheet[i].name = "walk_up_" + (i -60);
                else if(i > 68 && i < 78) spritesheet[i].name = "walk_left_" + (i-69);
                else if(i > 77 && i < 87) spritesheet[i].name = "walk_down_" + (i-78);
                else if(i > 86) spritesheet[i].name = "walk_right_" + (i-87);
            }
            //attack
            else if(i > 95 && i < 120) {
                if (i < 102) spritesheet[i].name = "attack_up_" + (i - 96);
                else if (i > 101 && i < 108) spritesheet[i].name = "attack_left_" + (i - 102);
                else if(i > 107 && i < 114) spritesheet[i].name = "attack_down_" + (i - 108);
                else if(i > 113) spritesheet[i].name = "attack_right_" + (i - 114);
            }
        }
        importer.spritesheet = spritesheet;

        EditorUtility.SetDirty(importer);
        importer.SaveAndReimport();
    }

    public static bool IsValidTargetForPallete() {
        if (Selection.activeObject == null) {
            Debug.LogAssertion("Need to select asset.");
            return false;
        }
        if (Selection.objects.Length > 1) {
            Debug.LogAssertion("Need to select only one asset.");
            return false;
        }
        if (Selection.activeObject.GetType() == typeof(Texture2D)) return true;
        
        Debug.LogAssertion("Need to select an asset type of Texture");
        return false;

    }

}
#endif