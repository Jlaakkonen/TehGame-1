using UnityEngine;
using System.Collections;

public class CatControl : MonoBehaviour {

	public float actionInterval;
	float catSpeed = -5f;
	public float catJumpHigh = 9f;
	public float catGroundTime = 1f;
	private Transform bottomShelf, middleShelf, topShelf;
	float actionTimer;
	float diff;
	float nextShelfPosition;
	public float screenWidthMultiplier = 0.8f;
	GameObject screenSize;
	int up,down,stay;
	bool catAir = false;
	bool facingLeft;
	GameObject[] items;
	Rigidbody2D rgb2D;
	SpriteRenderer sprite;
	Animator anim;
	bool jumpDirectionDown = false;
	bool action = false;
	float xmin, xmax;
	BoxCollider2D catCol;
	int catActive = 1;





	// Use this for initialization
	void Start () {
		rgb2D = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		sprite = GetComponent<SpriteRenderer> ();
		catCol = GetComponent<BoxCollider2D> ();
		items = GameObject.FindGameObjectsWithTag ("Items");
		xmin = ShelfGameManager.xMinPoint;
		xmax = ShelfGameManager.xMaxPoint;
		bottomShelf = GameObject.Find ("BottomShelf").transform;
		middleShelf = GameObject.Find ("MiddleShelf").transform;
		topShelf = GameObject.Find ("TopShelf").transform;
		if (catSpeed > 0) {
			facingLeft = false;
		} 
		else if (catSpeed < 0) {
			facingLeft = true;
		}
		catSpeed = -FitToCameraWidth.publicWidth * screenWidthMultiplier;
		anim.SetBool ("Jump", false);
		anim.SetBool ("Stop", false);
		actionTimer = actionInterval;
	}

	// Update is called once per frame
	void FixedUpdate () {
		//Moves cat if all conditions are true
		if (catAir == false) {
			if (action == false) {
				transform.position = new Vector3 (transform.position.x + catSpeed * Time.deltaTime, transform.position.y);
			}
		}
		//Stops cats jump and sets variables so that cat can move on landed shelf.
		else if (catAir == true) {
			if (rgb2D.velocity.y < 0f && nextShelfPosition >= transform.position.y) {
				anim.SetBool ("Jump", false);
				//rgb2D.gravityScale = 0f;
				catCol.enabled = true;
				rgb2D.velocity = Vector2.zero;
				anim.Play ("CatWalk");
				catAir = false;
				action = false;

			}
		}

		//Calculate time when cat needs to jump
		if (actionTimer > 0 && catAir == false && action == false) {
			actionTimer -= Time.deltaTime;
		} 
		else if (actionTimer <= 0) {
			catActive = Random.Range (1, 4);
			Debug.Log ("Case: " + catActive);
			switch (catActive) 
			{
			case 1:
				Debug.Log ("Nothing");
				break;

			case 2:
				Debug.Log ("Jump");
				CatJump ();
				break;

			case 3:
				Debug.Log ("Stop");
				CatLay ();
				break;
			}
			//CatJump ();
			
			actionTimer = actionInterval;
		}



		//Checks if cat hits the wall and if cat hits flips movement and sprite
		if (transform.position.x <= ShelfGameManager.xMinPoint && facingLeft == true) {
			catSpeed *= -1f;
			SpriteFlipper ();
			facingLeft = false;
			if (catAir == true) {
				rgb2D.velocity = new Vector2 (catSpeed, rgb2D.velocity.y);
			}
		} 
		else if (transform.position.x >= ShelfGameManager.xMaxPoint && facingLeft == false) {
			catSpeed *= -1f;
			SpriteFlipper ();
			facingLeft = true;
			if (catAir == true) {
				rgb2D.velocity = new Vector2 (catSpeed, rgb2D.velocity.y);
			}
		}

		//Clamp cats movement to screens sides
		transform.position = new Vector3 (Mathf.Clamp(transform.position.x,ShelfGameManager.xMinPoint,ShelfGameManager.xMaxPoint), transform.position.y);

	}

	void CatLay()
	{
		action = true;
		anim.SetBool ("Stop", true);
		Invoke ("CatStood", anim.GetCurrentAnimatorStateInfo(0).length + catGroundTime);
	}

	void CatStood()
	{
		anim.SetBool ("Stood", true);
		anim.SetBool ("Stop", false);
		Invoke ("CatWalk", anim.GetCurrentAnimatorStateInfo(0).length - 0.2f);
	}

	void CatWalk()
	{
		anim.SetBool ("Stood", false);
		action = false;
	}

	//Checks were all items are and sets values on that shelf variable
	void CatJump()
	{
		items = GameObject.FindGameObjectsWithTag ("Items");

		foreach (GameObject item in items) {
			diff = transform.position.y - item.transform.position.y;

			if (diff < -1) {
				up += 1;
			} 
			else if (diff > -1 && diff < 1) {
				stay += 1;
			} 
			else if (diff > 1) {
				down += 1;
			}
		}
		//Sets value for next shelf to jump
		if (up >= down && up > stay) {
			if (transform.position.y >= 12f && transform.position.y <= 14f) 
			{
				nextShelfPosition = topShelf.position.y;
			}
			else if(transform.position.y <= middleShelf.position.y)
			{
				nextShelfPosition = middleShelf.position.y;
			}
			anim.SetBool ("Jump",true);
			//anim.Play("CatJump");
			jumpDirectionDown = false;
			action = true;
			Invoke ("CatJumpPhys", anim.GetCurrentAnimatorStateInfo(0).length/3);
		} 
		else if (down >= up && down > stay) {
			if (transform.position.y >= 14f && transform.position.y <= 16f)
			{
				nextShelfPosition = middleShelf.position.y;
			}
			else if(transform.position.y >= 12f && transform.position.y <= 14f)
			{
				nextShelfPosition = bottomShelf.position.y;
			}
			anim.SetBool("Jump",true);
			//anim.Play("CatJump");
			jumpDirectionDown = true;
			action = true;
			Invoke ("CatJumpPhys", anim.GetCurrentAnimatorStateInfo(0).length/3);
		}
		//Resets cat jump timer
		else if (stay > up && stay > down) {
			actionTimer = actionInterval;
		}

		up = 0;
		stay = 0;
		down = 0;
		diff = 0;

	}

	//Makes cat jump
	void CatJumpPhys()
	{
		if (jumpDirectionDown == true) {
			catCol.enabled = false;
			rgb2D.velocity = new Vector2 (catSpeed, 2f);
			catAir = true;
		} 
		else if (jumpDirectionDown == false) {
			catCol.enabled = false;
			rgb2D.velocity = new Vector2(catSpeed,catJumpHigh);
			catAir = true;
		}
	}

	//Sprite flipper
	void SpriteFlipper()
	{
		if (sprite.flipX == enabled)
		{
			sprite.flipX = !enabled;
		}
		else if (sprite.flipX == !enabled)
		{
			sprite.flipX = enabled;
		}
	}
}
