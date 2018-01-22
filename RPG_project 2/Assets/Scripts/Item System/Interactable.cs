using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Interactable : MonoBehaviour {

    public float radius = 3f;
    private bool playerIsHere;
    protected string textToDisplay;
    private bool hasInteracted = false;

    void Awake() {
        CircleCollider2D coll = GetComponent<CircleCollider2D>();
        coll.radius = radius;
        coll.isTrigger = true;
        textToDisplay = "Press E to interact..";
    }

    public virtual void Interact() {
        Debug.Log("Interact with " + gameObject.name);
    }
	
	void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            UIManager.ToogleScreenText(textToDisplay);
            playerIsHere = true;
            PlayerController.SetInteractable(this);
        }
    }
    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            UIManager.ToogleScreenText("");
            playerIsHere = false;
            PlayerController.SetInteractable(null);
        }
    }
    
    

}
