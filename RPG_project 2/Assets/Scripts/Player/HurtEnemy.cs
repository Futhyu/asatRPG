using UnityEngine;
using System.Collections;

public class HurtEnemy : MonoBehaviour {

    
    private float currentDamage;
    public GameObject damageBurst;
    public Transform hitPoint;

    public GameObject damageNum;

    private PlayerController thePS;

	void Start () {
        thePS = FindObjectOfType<PlayerController>();
	}

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Enemy" && !other.isTrigger) {

            currentDamage = thePS.attackDamage;
            
            //Destroy(other.gameObject);
            other.gameObject.GetComponent<CharacterStats>().TakeDamage(currentDamage);
            //Instantiate(damageBurst, hitPoint.position, hitPoint.rotation);
            //var clone = (GameObject) Instantiate(damageNum, hitPoint.position, Quaternion.identity);
            //clone.GetComponent<FloatingNumbers>().damageNumber = currentDamage;
        }
    }

}
