using UnityEngine;
using System.Collections;

public class HurtPlayer : MonoBehaviour {

    public int damageToGive;
    private int currentDamage;
    public GameObject damageNumber;

    

	void Start () {
        
	}
	
	void Update () {
	
	}
    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player") {

            currentDamage = damageToGive;
            if(currentDamage < 0) {
                currentDamage = 0;
            }

            other.gameObject.GetComponent<PlayerStats>().TakeDamage(currentDamage);

            var clone = (GameObject)Instantiate(damageNumber, other.transform.position, Quaternion.identity);
            clone.GetComponent<FloatingNumbers>().damageNumber = damageToGive;
        }
    }
}
