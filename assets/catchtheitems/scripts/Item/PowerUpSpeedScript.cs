using UnityEngine;
using System.Collections;

public class PowerUpSpeedScript : MonoBehaviour {

	private Rigidbody2D rgb2D;
	public float rotationSpeed;
	private SpriteRenderer spriteRenderer;
	private PlayerMovement playerMovement;

	// Use this for initialization
	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer> ();
		rgb2D = gameObject.GetComponent<Rigidbody2D> ();
		playerMovement = FindObjectOfType<PlayerMovement> ();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Player") 
		{
			rgb2D.constraints = RigidbodyConstraints2D.FreezePositionY;

			spriteRenderer.enabled = false;

			playerMovement.moveSpeed = 6f;

			Invoke ("PlayerMovementBack", 3);

		}

		else if (col.tag == "Floor")
		{
			Destroy (this.gameObject);
		}
	}

	public void Activate()
	{
		rgb2D.AddTorque (rotationSpeed);
		rgb2D.gravityScale = 1.5f;
	}

	void PlayerMovementBack()
	{
		playerMovement.moveSpeed = 5f;
		Destroy (gameObject);
	}


}
