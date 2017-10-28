//using UnityEngine;
//using System.Collections;

//public class VillagerMovement : MonoBehaviour {

//    public float moveSpeed;
    
//    private Vector2 minWalkPoint;
    
//    private Vector2 maxWalkPoint;

//    private Rigidbody2D myRigidbody;

//    public bool isWalking;
//    public float walkTime;
//    public float waitTime;

//    private int WalkDirection;

//    public Collider2D walkZone;
    
//    private bool hasWalkZone;

//    public bool canMove;

//    private DialogueManager theDM;

//	void Start () {
//        myRigidbody = GetComponent<Rigidbody2D>();
//        theDM = FindObjectOfType<DialogueManager>();

//        ChooseDirection();

//        if (walkZone != null) {
//            minWalkPoint = walkZone.bounds.min;
//            maxWalkPoint = walkZone.bounds.max;
//            hasWalkZone = true;
//        }

//        canMove = true;
//	}
	
	
//	void Update () {

//        if (!theDM.dialogActive) {
//            canMove = true;
//        }

//        if (!canMove) {
//            myRigidbody.velocity = Vector2.zero;
//            return;
//        }

//        if (isWalking) {
//            switch (WalkDirection) {
//                case 0:
//                    myRigidbody.velocity = new Vector2(0f, moveSpeed);
//                    if (hasWalkZone && (transform.position.y > maxWalkPoint.y)) {
//                         StopCoroutine("Walk");
//                         isWalking = false;
                         
//                    }
//                    break;
//                case 1:
//                    myRigidbody.velocity = new Vector2(moveSpeed, 0);

//                    if (hasWalkZone && (transform.position.x > maxWalkPoint.x)) {
//                        StopCoroutine("Walk");
//                        isWalking = false;
//                    }
//                    break;
//                case 2:
//                    myRigidbody.velocity = new Vector2(0f, -moveSpeed);

//                    if (hasWalkZone && (transform.position.y < minWalkPoint.y)) {
//                        StopCoroutine("Walk");
//                        isWalking = false;
//                    }
//                    break;
//                case 3:
//                    myRigidbody.velocity = new Vector2(-moveSpeed, 0);
//                    if (hasWalkZone && (transform.position.x < minWalkPoint.x)) {
//                        StopCoroutine("Walk");
//                        isWalking = false;

//                    }
//                    break;
//            }
//            if (isWalking) {
                
//                StartCoroutine("Walk");
                
//            }

//            else {
//                myRigidbody.velocity = Vector2.zero;
                
//                StartCoroutine("Stop");
                
                
//            }
//        }
//        else {
//            myRigidbody.velocity = Vector2.zero;
//            StartCoroutine("Stop");
//            ChooseDirection();
//        }
//    }

//    IEnumerator Walk() {
        
//        yield return new WaitForSeconds(walkTime);
//        isWalking = false;

//    }

//    IEnumerator Stop() {
        
//        yield return new WaitForSeconds(waitTime);
//        isWalking = true;
        
//    }

//    public void ChooseDirection() {
//        WalkDirection = Random.Range(0, 4);
//        //isWalking = true;
//        }
//    }



