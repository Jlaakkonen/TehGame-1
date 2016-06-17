using UnityEngine;
using System.Collections;

public class DropItem : MonoBehaviour {

	protected Rigidbody2D rgb2D;
	protected BoxCollider2D itemColl;
	public float rotationSpeed;
	float destPointY;
	//public static Transform destroyPoint;


	// Use this for initialization
	protected virtual void Start () 
	{
		rgb2D = gameObject.GetComponent<Rigidbody2D> ();
		itemColl = gameObject.GetComponent<BoxCollider2D> ();
		itemColl.isTrigger = true;
		destPointY = ShelfGameManager.destroyPoint.position.y;
	}

	protected virtual void Update()
	{
		if (transform.position.y <= 8f) {
			itemColl.isTrigger = false;
		}
		if (transform.position.y <= destPointY) {
			Points.breakingPoints++;
			Destroy (gameObject);
		}
	}

	//Apply physics in child item
	public void Activate()
	{
		rgb2D.gravityScale = 1.5f;
		rgb2D.AddTorque (rotationSpeed);
	}

	public void WaitActivate()
	{
		transform.parent = null;
		Invoke ("Activate", 0.7f);
	}
}
