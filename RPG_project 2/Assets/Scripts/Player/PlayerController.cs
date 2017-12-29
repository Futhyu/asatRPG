using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public static PlayerController main;
    private PlayerStats thePS;
    
    private Animator anim;
    private Rigidbody2D myRigidbody;

    public float moveSpeed;
    private bool isMove;
    public Vector2 lastMove;
    public Quaternion lastRotation;
    private Vector2 moveInput;
    public bool canMove;

    //private static bool playerExists;
    private Interactable interactable;
    //WEAPON
    [Header("Fight System")]
    public Transform weaponSlot;
    public Transform attack;
    public float attackRadius;
    private bool isAttacking;
    public bool isCasting { private get; set; }
    public float attackTime;
    public float attackDamage { get; private set; }

    [Header("Equipment System")]
    public Transform head;
    public Transform chest;
    public Transform arms;
    public Transform legs;
    public Transform foot;

    public string startPoint;
    
    //public float strength { get; private set; }
    //public float agility { get; private set; }
    //public float intelligence { get; private set; }
    //public float faith { get; private set; }
    //public float rate { get; private set; }

    //private Transform questGrid;

    void Awake() {
        
        if (main == null)
            main = this;
        
        anim = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        thePS = GetComponent<PlayerStats>();
        canMove = true;
        lastMove = new Vector2(0, -1);
        Invoke("SetAttackDamage", 0.5f);
        //rate = 1;
        //strength = 10;
        //agility = 13;
        //intelligence = 18;
        //faith = 14;
    }

    void Update() {
        if (!canMove) {
            myRigidbody.velocity = Vector2.zero;
            //return;
        }

        if (!isAttacking && canMove) {
            // if not attack then move
            moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

            if (moveInput != Vector2.zero) {
                myRigidbody.velocity = new Vector2(moveInput.x * moveSpeed, moveInput.y * moveSpeed);
                isMove = true;
                lastMove = moveInput;
                float angle = Mathf.Atan2(lastMove.y, lastMove.x) * Mathf.Rad2Deg;
                lastRotation = Quaternion.AngleAxis(angle, Vector3.forward);
                
            }
            else {
                myRigidbody.velocity = Vector2.zero;
                isMove = false;
            }
        }
        anim.SetBool("isAttacking", isAttacking);
        anim.SetBool("isMoving", isMove);
        anim.SetBool("isCasting", isCasting);
        anim.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
        anim.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));
        anim.SetFloat("LastMoveX", lastMove.x);
        anim.SetFloat("LastMoveY", lastMove.y);
        EquipmentAnimator.instance.Animate(moveInput, lastMove, isMove, isAttacking, isCasting);
        GetInput();
    }
    
    private void SetAttackDamage() {
        attackDamage = GameLogic.CalculatePlayerBaseAttackDamage(this, thePS); 
    }

    #region Equiping
    public void Equip(Equipment item) {
        if (item != null) {
            if (item.equipSlot == EquipmentSlot.Weapon) {
                weaponSlot.GetComponent<Animator>().runtimeAnimatorController = item.controller;
                SetAttackDamage();
            }
            else if (item.equipSlot == EquipmentSlot.Chest) {
                chest.GetComponent<Animator>().runtimeAnimatorController = item.controller;
            }
            else if (item.equipSlot == EquipmentSlot.Arms) {
                arms.GetComponent<Animator>().runtimeAnimatorController = item.controller;
            }
            else if (item.equipSlot == EquipmentSlot.Head) {
                head.GetComponent<Animator>().runtimeAnimatorController = item.controller;
            }
            else if (item.equipSlot == EquipmentSlot.Legs) {
                legs.GetComponent<Animator>().runtimeAnimatorController = item.controller;
            }
            else if (item.equipSlot == EquipmentSlot.Foot) {
                foot.GetComponent<Animator>().runtimeAnimatorController = item.controller;
            }
        }
    }

    public void Unequip(Equipment item) {
        if (item != null) {
            if (item.equipSlot == EquipmentSlot.Weapon) {
                weaponSlot.GetComponent<Animator>().runtimeAnimatorController = null;
                weaponSlot.GetComponent<SpriteRenderer>().sprite = null;
                SetAttackDamage();
            }
            else if (item.equipSlot == EquipmentSlot.Chest) {
                chest.GetComponent<Animator>().runtimeAnimatorController = null;
                chest.GetComponent<SpriteRenderer>().sprite = null;
            }
            else if (item.equipSlot == EquipmentSlot.Arms) {
                arms.GetComponent<Animator>().runtimeAnimatorController = null;
                arms.GetComponent<SpriteRenderer>().sprite = null;
            }
            else if (item.equipSlot == EquipmentSlot.Head) {
                head.GetComponent<Animator>().runtimeAnimatorController = null;
                head.GetComponent<SpriteRenderer>().sprite = null;
            }
            else if (item.equipSlot == EquipmentSlot.Legs) {
                legs.GetComponent<Animator>().runtimeAnimatorController = null;
                legs.GetComponent<SpriteRenderer>().sprite = null;
            }
            else if (item.equipSlot == EquipmentSlot.Foot) {
                foot.GetComponent<Animator>().runtimeAnimatorController = null;
                foot.GetComponent<SpriteRenderer>().sprite = null;
            }
        }
    }
    #endregion

    void GetInput() {

        if (Input.GetButtonDown("Attack")) {
            if (!isAttacking) {
                StartCoroutine("Attack");
            }
            else
                Debug.Log("Not equiped");
        }
        if (Input.GetButtonDown("Interact")) {
            if(interactable != null) {
                interactable.Interact();
            }
        }
        //if (Input.GetKeyDown(KeyCode.G)) {
        //    questGrid.gameObject.SetActive(true);
        //}

    }
    
    public void Stun(float time) {

    }

    public static void SetInteractable(Interactable interactable) {
        main.interactable = interactable;
    }

    IEnumerator Attack() {
        isAttacking = true;
        myRigidbody.velocity = Vector2.zero;
        yield return new WaitForSeconds(attackTime);
        Fight2D.Action(attack.position, attackRadius, 8, 1, false);
        isAttacking = false;
        //Input.
    }

}


