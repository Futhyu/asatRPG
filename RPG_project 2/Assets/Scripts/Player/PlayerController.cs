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
    [Header("Weapon")]
    private GameObject equipedWeapon;
    public Transform weaponSlot;
    private bool isAttacking;
    public float attackTime;
    public float attackDamage { get; private set; }
    
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
        weaponSlot = GameObject.Find("Player/Weapon").transform;

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
        isMove = false;

        if (!canMove) {
            myRigidbody.velocity = Vector2.zero;
            return;
        }

        if (!isAttacking) {
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
            }
        }
        
        anim.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
        anim.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));
        anim.SetBool("isMoving", isMove);
        anim.SetFloat("LastMoveX", lastMove.x);
        anim.SetFloat("LastMoveY", lastMove.y);
        GetInput();
    }

    private void SetAttackDamage() {
        attackDamage = GameLogic.CalculatePlayerBaseAttackDamage(this, thePS); 
    }

    public void Equip(Equipment weapon) {
        if (equipedWeapon != null) {
            Destroy(weaponSlot.GetChild(0).gameObject);
        }
        if (weapon != null) {
            equipedWeapon = (GameObject)Instantiate(Resources.Load<GameObject>("Prefabs/Weapons/" + weapon.name), weaponSlot);

            equipedWeapon.transform.SetParent(weaponSlot);
            equipedWeapon.transform.position = weaponSlot.transform.position;
            equipedWeapon.transform.rotation = weaponSlot.transform.rotation;
        }
        SetAttackDamage();
    }

    void GetInput() {

        if (Input.GetKeyUp(KeyCode.F)) {
            if (equipedWeapon != null) {
                StartCoroutine("Attack");
            }
            else
                Debug.Log("Not equiped");
        }
        if (Input.GetKeyUp(KeyCode.E)) {
            if(interactable != null) {
                interactable.Interact();
            }
        }
        //if (Input.GetKeyDown(KeyCode.G)) {
        //    questGrid.gameObject.SetActive(true);
        //}

    }
    
    public static void SetInteractable(Interactable interactable) {
        main.interactable = interactable;
    }

    IEnumerator Attack() {
        isAttacking = true;
        myRigidbody.velocity = Vector2.zero;
        anim.SetBool("isAttack", true);
        yield return new WaitForSeconds(attackTime);
        isAttacking = false;
        anim.SetBool("isAttack", false);
        
        //Input.
    }
    
}


