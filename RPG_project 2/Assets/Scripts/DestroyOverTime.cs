using UnityEngine;
using System.Collections;

public class DestroyOverTime : MonoBehaviour {

    public float timeToDestroy;

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        timeToDestroy -= Time.deltaTime;

        if (timeToDestroy <= 0) {
            Destroy(gameObject);
        }
	}
}
