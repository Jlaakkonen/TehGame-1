using UnityEngine;
using System.Collections;

public class SizeItem : DropItem {

	private Catcher bucketScript;

	protected override void Start ()
	{
		base.Start ();
		bucketScript = FindObjectOfType<Catcher> ();
	}

	protected override void Update()
	{
		if (transform.position.y <= ShelfGameManager.destroyPoint.position.y) {
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.tag == "Bucket")
		{
			bucketScript.LargeBucket ();
		}
	}
}
