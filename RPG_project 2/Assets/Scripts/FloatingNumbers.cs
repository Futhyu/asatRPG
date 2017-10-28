using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FloatingNumbers : MonoBehaviour {

    public float moveSpeed;
    public float damageNumber;
    public Text displayNum;

	void Update () {

        displayNum.text = "" + damageNumber;
        transform.position = new Vector3(transform.position.x, transform.position.y + (moveSpeed * Time.deltaTime), transform.position.z);

	}
}
