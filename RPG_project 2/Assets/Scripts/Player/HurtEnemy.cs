using UnityEngine;

public class HurtEnemy : MonoBehaviour {
    
    private float currentDamage;
    public GameObject damageBurst;

    public GameObject damageNum;

    private PlayerController thePS;

	void Start () {
        thePS = FindObjectOfType<PlayerController>();
	}

    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log(other.gameObject.name);
        if (!other.isTrigger && other.gameObject.CompareTag("Enemy")) {
            currentDamage = thePS.attackDamage;
            //Destroy(other.gameObject);
            other.gameObject.GetComponent<CharacterStats>().TakeDamage(currentDamage);
            //Instantiate(damageBurst, hitPoint.position, hitPoint.rotation);
            //var clone = (GameObject) Instantiate(damageNum, hitPoint.position, Quaternion.identity);
            //clone.GetComponent<FloatingNumbers>().damageNumber = currentDamage;
        }
    }

    //void OnCollisionEnter2D(Collision2D coll) {
    //    Debug.Log(1);
    //    if (coll.gameObject.CompareTag("Enemy")) {
    //        coll.gameObject.SendMessage("TakeDamage", currentDamage);
    //    }
    //}

}
