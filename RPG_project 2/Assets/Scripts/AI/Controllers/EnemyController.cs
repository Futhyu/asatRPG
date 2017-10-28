using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
    
    protected PlayerController thePlayer;
    protected CircleCollider2D viewPlane;

    protected Rigidbody2D myRigidbody;
    protected Animator anim;
    protected Vector3 moveDirection;
    protected Vector2 lastMove;

    private float timeBetweenMove;
    private float timeToMove;
    
    [SerializeField]
    protected float moveSpeed;
    protected bool isMoving;

    protected bool isAttack;
    protected bool isCasting;
    [SerializeField]
    protected Ability ability;
    [SerializeField]
    protected bool friendly;
    protected bool isSeeing;
    protected Vector2 toPlayer;
    protected float distanceToPlayer;
    [SerializeField]
    protected float viewDistance;
    [SerializeField]
    protected float attackTime;
    [SerializeField]
    protected float cost;
    
    protected bool isConfused;
    private float timeStun;

    void Awake() {
        
        myRigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        thePlayer = FindObjectOfType<PlayerController>();
    }

	void Start () {
        //viewPlane = GetComponent<CircleCollider2D>();
        isConfused = false;
        timeBetweenMove = 1f;
        timeToMove = 2f;
        isAttack = false;
    }

    void Update() {
        Activity(timeToMove, timeBetweenMove);
    }

    protected void Activity(float timeToMove, float timeBetweenMove) {
        if (isSeeing && !friendly) {
            Attack();
        }
        else if (!isConfused && !isAttack) {
            if (isMoving) {

                StartCoroutine(Move(timeToMove));
            }
            else {
                StartCoroutine(Stop(timeBetweenMove));
            }
            //stun?
        }
        if (isConfused) {
            StartCoroutine(StunTime(timeStun));
            StartCoroutine(Stop(timeStun));
        }
    }

    protected virtual void Attack() {
        Debug.LogWarning("Attack by base script EnemyController on " + gameObject.name + "!");
    }
    
    protected virtual void CastAbility(Ability ability) {

        if (ability == null) return;

        else if (isCasting) return;

        else {
            if(ability.type == Ability.AbilityType.Range) {
                GameObject abilityObj = (GameObject)Instantiate(ability.spellPrefab, transform.position, Camera.main.GetComponent<Transform>().rotation, transform.GetChild(0));
                //abilityObj.GetComponent<Animator>().SetBool("isFireball", ability.abilitySpell.abilityName == "Fireball");
                
                Vector2 dir = GetComponent<EnemyController>().MoveDirection;
                var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                abilityObj.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                abilityObj.GetComponent<Rigidbody2D>().gravityScale = 0;
                abilityObj.GetComponent<Rigidbody2D>().velocity = dir.normalized * ability.projectileSpeed;
            }
        }

    }

    public void Stun(float timeStun) {
        isConfused = true;
        this.timeStun = timeStun;
    }
    
    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) isSeeing = true;
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) isSeeing = false;
    }

    #region Coroutines
    protected virtual IEnumerator AttackPlayer() {
        yield return null;
    }

    private IEnumerator Move(float timeToMove) {

        if (myRigidbody.velocity == Vector2.zero) myRigidbody.velocity = moveDirection;
        yield return new WaitForSeconds(timeToMove);
        isMoving = false;
    }

    private IEnumerator Stop(float timeToWait) {
        myRigidbody.velocity = Vector2.zero;
        lastMove = moveDirection;
        moveDirection = Vector3.zero;
        yield return new WaitForSeconds(timeToWait);
        if(moveDirection == Vector3.zero) moveDirection = new Vector3(Random.Range(-1, 1) * moveSpeed, Random.Range(-1, 1) * moveSpeed, 0);
        isMoving = true;
        
    }
    
    private IEnumerator StunTime(float timeStun) {
        yield return new WaitForSeconds(timeStun);
        isConfused = false;
    }
    #endregion
    
    public bool IsConfused { set { isConfused = value; } }
    public Vector3 MoveDirection { get { return moveDirection; } }
    public float MoveSpeed { get { return moveSpeed; } set { moveSpeed = value; } }
}
