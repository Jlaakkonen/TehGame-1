using UnityEngine;
using System.Collections;

public class SpeedItem : DropItem {

	private SpriteRenderer spriteRenderer;
	private PlayerMovement playerMovement;

	// Use this for initialization
	protected override void Start ()
	{
		base.Start ();
		spriteRenderer = GetComponent<SpriteRenderer> ();
		playerMovement = FindObjectOfType<PlayerMovement> ();
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		if (col.collider.tag == "Player") 
		{
			rgb2D.constraints = RigidbodyConstraints2D.FreezePositionY;

			spriteRenderer.enabled = false;

			playerMovement.moveSpeed = 1.5f;

			Invoke ("PlayerMovementBack", 3);
		}

		else if (col.collider.tag == "Floor")
		{
			Destroy (this.gameObject);
		}
	}

	void PlayerMovementBack()
	{
		playerMovement.moveSpeed = 1f;
		Destroy (gameObject);
	}
}
