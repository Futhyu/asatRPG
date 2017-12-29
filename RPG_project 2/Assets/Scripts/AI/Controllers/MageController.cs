using System.Collections;
using UnityEngine;

public class MageController : EnemyController {

    void Start() {
        anim = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        thePlayer = FindObjectOfType<PlayerController>();
        viewPlane = GetComponentInChildren<CircleCollider2D>();
        isCasting = false;
        viewPlane.radius = viewDistance;
    }

    void Update() {
        
        //toPlayer = thePlayer.transform.position - transform.position;
        Activity(1f, 2f);

        anim.SetBool("isMoving", isMoving);
        anim.SetBool("isCasting", isCasting);
        anim.SetBool("isStunned", isConfused);
        anim.SetFloat("MoveX", moveDirection.normalized.x);
        anim.SetFloat("MoveY", moveDirection.normalized.y);
        anim.SetFloat("LastMoveX", lastMove.normalized.x);
        anim.SetFloat("LastMoveY", lastMove.normalized.y);
    }

    protected override void Attack() {
        toPlayer = thePlayer.transform.position - transform.position;
        float distanceToPlayer = toPlayer.magnitude; 
        //if player so far
        if (distanceToPlayer > 5f) {
            //if cast and move then stop casting and go kill
            if (isCasting) {
                StopAllCoroutines();
                isCasting = false;
                isAttack = false;
            }
            isMoving = true;
            lastMove = moveDirection;
            moveDirection = toPlayer.normalized;
            myRigidbody.velocity = moveDirection * moveSpeed;
        }
        //if player near
        else if (distanceToPlayer <= 5f) {
            isMoving = false;
            lastMove = moveDirection;
            moveDirection = toPlayer.normalized;
            myRigidbody.velocity = Vector2.zero;
            if (!isAttack) StartCoroutine(AttackPlayer());
            
        }
    }

    protected override IEnumerator AttackPlayer() {
        isAttack = true;
        isCasting = true;
        yield return new WaitForSeconds(ability.castTime);
        isCasting = false;
        CastAbility(ability);
        yield return new WaitForSeconds(attackTime);
        isAttack = false;
    }
    
}
