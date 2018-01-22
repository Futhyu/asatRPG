#if UNITY_EDITOR
using UnityEditor;

using UnityEngine;
using System.Collections.Generic;

public class AnimationEquipmentEditor : EditorWindow {

    string animationClipName;
    string path;
    AnimationClip animClip;
    AnimationType animType = AnimationType.Walk;
    List<Sprite> sprites;
    float time = 0;

    [MenuItem("Editors/Animation Editor")]
    public static void ShowWindow() {
        GetWindow<AnimationEquipmentEditor>("Sprite Editor");

    }

    void OnGUI() {

        if (animClip == null) {

            GUILayout.Label("Equipment Animation", EditorStyles.boldLabel);

            if (GUILayout.Button("Create New Animation")) {
                animClip = new AnimationClip();
                sprites = new List<Sprite>();
            }
        }

        else {
            GUILayout.Label("Animation name", EditorStyles.boldLabel);
            animationClipName = EditorGUILayout.TextField(animationClipName);
            if (path == null) {
                GUILayout.Space(10);
                GUILayout.Label("Path", EditorStyles.boldLabel);
                if (GUILayout.Button("Get Path")) {
                    if (IsValidTargetForPallete(typeof(UnityEditor.Animations.AnimatorController))) {
                        path = AssetDatabase.GetAssetPath(Selection.activeObject);
                    }
                }
            }
            GUILayout.Space(10);
            if (animType == AnimationType.Walk) {
                ObjectReferenceKeyframe[] ks = new ObjectReferenceKeyframe[9];
                GUILayout.Label("Walk", EditorStyles.boldLabel);
                if (sprites.Count > 8) {
                    EditorCurveBinding spriteBinding = new EditorCurveBinding();
                    spriteBinding.type = typeof(SpriteRenderer);
                    spriteBinding.path = "";
                    spriteBinding.propertyName = "m_Sprite";

                    for (int i = 0; i < sprites.Count; i++) {
                        ks[i] = new ObjectReferenceKeyframe();
                        ks[i].time = time;
                        ks[i].value = sprites[i];
                        time += ((0.1f * 5) / 6);
                    }
                    
                    AnimationUtility.SetObjectReferenceCurve(animClip, spriteBinding, ks);
                    sprites.Clear();
                }
            }
            else if (animType == AnimationType.Idle) {
                ObjectReferenceKeyframe[] ks = new ObjectReferenceKeyframe[1];
                GUILayout.Label("Idle", EditorStyles.boldLabel);
                if (sprites.Count > 0) {
                    animClip.frameRate = 1;
                    EditorCurveBinding spriteBinding = new EditorCurveBinding();
                    spriteBinding.type = typeof(SpriteRenderer);
                    spriteBinding.path = "";
                    spriteBinding.propertyName = "m_Sprite";

                    ks[0] = new ObjectReferenceKeyframe();
                    ks[0].time = 0;
                    ks[0].value = sprites[0];

                    AnimationUtility.SetObjectReferenceCurve(animClip, spriteBinding, ks);
                    sprites.Clear();
                }
            }
            else if (animType == AnimationType.Slash) {
                ObjectReferenceKeyframe[] ks = new ObjectReferenceKeyframe[6];
                GUILayout.Label("Slash", EditorStyles.boldLabel);
                if (sprites.Count > 5) {
                    EditorCurveBinding spriteBinding = new EditorCurveBinding();
                    spriteBinding.type = typeof(SpriteRenderer);
                    spriteBinding.path = "";
                    spriteBinding.propertyName = "m_Sprite";

                    for (int i = 0; i < sprites.Count; i++) {
                        ks[i] = new ObjectReferenceKeyframe();
                        ks[i].time = time;
                        ks[i].value = sprites[i];
                        time += ((0.1f * 5) / 6);
                    }

                    AnimationUtility.SetObjectReferenceCurve(animClip, spriteBinding, ks);
                    sprites.Clear();
                }
            }
            else if (animType == AnimationType.Cast) {
                ObjectReferenceKeyframe[] ks = new ObjectReferenceKeyframe[7];
                GUILayout.Label("Cast", EditorStyles.boldLabel);
                if (sprites.Count > 6) {
                    EditorCurveBinding spriteBinding = new EditorCurveBinding();
                    spriteBinding.type = typeof(SpriteRenderer);
                    spriteBinding.path = "";
                    spriteBinding.propertyName = "m_Sprite";

                    for (int i = 0; i < sprites.Count; i++) {
                        ks[i] = new ObjectReferenceKeyframe();
                        ks[i].time = time;
                        ks[i].value = sprites[i];
                        time += ((0.1f * 5) / 6);
                    }

                    AnimationUtility.SetObjectReferenceCurve(animClip, spriteBinding, ks);
                    sprites.Clear();
                }
            }
            animType = (AnimationType)EditorGUILayout.EnumPopup(animType);
            // Assets/Animations/Equipment/Armor/ for exaample
            if (!animClip.empty) {
                if (GUILayout.Button("Create and save animation")) {
                    AnimationClipSettings settings = AnimationUtility.GetAnimationClipSettings(animClip);
                    settings.loopTime = true;
                    AnimationUtility.SetAnimationClipSettings(animClip, settings);
                    AssetDatabase.CreateAsset(animClip, path.Remove(path.LastIndexOf('/') + 1) + animationClipName + ".anim");

                    AssetDatabase.SaveAssets();
                }
            }
            else {
                GUILayout.Space(10);
                GUILayout.Label("Sprite Animation", EditorStyles.boldLabel);

                if (GUILayout.Button("Add sprite")) {
                    if (IsValidTargetForPallete(typeof(Sprite))) {
                        sprites.Add((Sprite)Selection.activeObject);
                    }
                }
                if ((sprites.Count < 9 && animType == AnimationType.Walk) || (sprites.Count < 1 && animType == AnimationType.Idle)) {
                    for (int i = 0; i < sprites.Count; i++) {
                        GUILayout.Label(sprites[i].name);
                    }
                }
            }
        }
    }
    public static bool IsValidTargetForPallete(System.Type type) {
        if (Selection.activeObject == null) {
            Debug.LogAssertion("Need to select asset.");
            return false;
        }
        if (Selection.objects.Length > 1) {
            Debug.LogAssertion("Need to select only one asset.");
            return false;
        }
        if (Selection.activeObject.GetType() == type) return true;

        Debug.LogAssertion("Need to select an asset type of " + type.Name + ". Not " + Selection.activeObject.GetType().Name);
        return false;

    }
}
    public enum AnimationType { Idle, Walk, Slash, Cast }
#endif