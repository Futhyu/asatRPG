using UnityEngine;

public class EquipmentAnimator : MonoBehaviour {

    #region Singleton
    public static EquipmentAnimator instance;

    void Awake() {
        if(instance == null) instance = this;
    }
    #endregion

    private Transform[] equipment;

    void Start() {
        equipment = GetComponentsInChildren<Transform>();
    }

    public void Animate(Vector2 move, Vector2 lastMove, bool isMoving, bool isAttacking, bool isCasting) {
        Animator anim;
        foreach (Transform item in equipment) {
            if (!item.GetComponent<Animator>()) continue;
            anim = item.GetComponent<Animator>();
            
            if (anim.runtimeAnimatorController == null) continue;
            
            anim.SetBool("isAttacking", isAttacking);
            anim.SetBool("isMoving", isMoving);
            anim.SetBool("isCasting", isCasting);
            anim.SetFloat("MoveX", move.x);
            anim.SetFloat("MoveY", move.y);
            anim.SetFloat("LastMoveX", lastMove.x);
            anim.SetFloat("LastMoveY", lastMove.y);
        }
    }

}
