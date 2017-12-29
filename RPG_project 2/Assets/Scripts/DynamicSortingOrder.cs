using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class DynamicSortingOrder : MonoBehaviour {

    private SpriteRenderer spriteRenderer;
    private int meshID;

	void Start () {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        meshID = spriteRenderer.sortingOrder;
	}
	
	void LateUpdate () {
        spriteRenderer.sortingOrder = meshID + (int)Camera.main.WorldToScreenPoint(spriteRenderer.bounds.min).y * -1;
    }
}
