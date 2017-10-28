using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadNewArea : MonoBehaviour {

    
    public string levelToLoad;

    public string exitPoint;

    private PlayerController thePlayer;

	void Start () {
        thePlayer = FindObjectOfType<PlayerController>();
        Debug.Log(gameObject.name);
	}
	

	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player") {
            SceneManager.LoadScene(levelToLoad);
            thePlayer.startPoint = exitPoint;
        }
    }

}
