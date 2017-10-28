using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public GameObject followTarget;
    private Vector3 targetPosition;
    public float moveSpeed;

    private static bool cameraExists;

	void Start () {
        if (!cameraExists) {
            cameraExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }
	

	void Update () {
        if (followTarget != null) {
            targetPosition = new Vector3(followTarget.transform.position.x, followTarget.transform.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.fixedDeltaTime);
        }
    }
}
