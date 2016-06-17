using UnityEngine;
using System.Collections;

public class Snowflake : MonoBehaviour {

	private PlayerMovement player;
	private Rigidbody2D rigid2D;
	private SpriteRenderer spriteRenderer;

	void Start()
	{
		player = FindObjectOfType<PlayerMovement> ();
	}

	void Update()
	{
		if (transform.position.y <= ShelfGameManager.destroyPoint.position.y) {
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Bucket")
		{
			player.Freeze ();
			Destroy (gameObject);
		}
	}
}
		

