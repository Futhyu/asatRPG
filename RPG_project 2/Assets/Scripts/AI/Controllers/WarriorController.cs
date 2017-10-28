using UnityEngine;
using System.Collections;

public class WarriorController : EnemyController {
    
	void Start () {
        anim = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        thePlayer = FindObjectOfType<PlayerController>();
        viewPlane = GetComponent<CircleCollider2D>();
        viewPlane.radius = viewDistance;
	}
    
    void Update() {
        
        Activity(1f, 2f);
        
        anim.SetBool("isMoving", isMoving);
        anim.SetBool("isAttacking", isAttack);
        anim.SetBool("isStunned", isConfused);
        anim.SetFloat("MoveX", moveDirection.normalized.x);
        anim.SetFloat("MoveY", moveDirection.normalized.y);
        anim.SetFloat("LastMoveX", lastMove.normalized.x);
        anim.SetFloat("LastMoveY", lastMove.normalized.y);
    }

    protected override IEnumerator AttackPlayer() {
        isAttack = true;
        thePlayer.GetComponent<PlayerStats>().TakeDamage(1);
        yield return new WaitForSeconds(attackTime);
        isAttack = false;
    }

    protected override void Attack() {
        float distanceToPlayer = Vector2.Distance(transform.position, thePlayer.transform.position);
        if (distanceToPlayer > 1.4f) {
            isMoving = true;
            //Vector2 toPlayer = Vector2.Lerp( thePlayer.transform.position, transform.position, 0.5f );
            Vector2 toPlayer = thePlayer.transform.position - transform.position;
            lastMove = moveDirection;
            moveDirection = toPlayer.normalized;
            myRigidbody.velocity = moveDirection * moveSpeed;
            //float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            //lastRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }else if(distanceToPlayer <= 1.4f){
            isMoving = false;
            myRigidbody.velocity = Vector2.zero;
            //isAttack = true;
            
            if(!isAttack)StartCoroutine(AttackPlayer());
            //isAttack = false;
            
        }
    }

}
