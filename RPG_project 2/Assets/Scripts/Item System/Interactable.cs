using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Interactable : MonoBehaviour {

    public float radius = 3f;
    private bool playerIsHere;

    private bool hasInteracted = false;

    void Awake() {
        CircleCollider2D coll = GetComponent<CircleCollider2D>();
        coll.radius = radius;
        coll.isTrigger = true;
    }

    public virtual void Interact() {
        Debug.Log("Interact with " + gameObject.name);
    }
	
	void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            UIManager.instance.screenText.gameObject.SetActive(true);
            playerIsHere = true;
            PlayerController.SetInteractable(this);
        }
    }
    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            UIManager.instance.screenText.gameObject.SetActive(false);
            playerIsHere = false;
            PlayerController.SetInteractable(null);
        }
    }
    
}
