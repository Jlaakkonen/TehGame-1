using UnityEngine;
using System.Collections;

public class CatController : MonoBehaviour {

	public float jumpInterval;
	float catSpeed = -5f;
	public float catJumpHigh = 9f;
	public Transform bottomShelf, middleShelf, topShelf;
	float jumpTimer;
	float diff;
	float nextShelfPosition;
	public float screenWidthMultiplier = 0.8f;
	GameObject screenSize;
	int up,down,stay;
	bool catAir = false;
	public bool facingLeft;
	GameObject[] items;
	Rigidbody2D rgb2D;
	SpriteRenderer sprite;
	Animator anim;
	bool jumpDirectionDown = false;
	public Sprite jumpSprite;
	bool jumping = false;
	float xmin, xmax;



	// Use this for initialization
	void Start () {
		rgb2D = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		sprite = GetComponent<SpriteRenderer> ();
		items = GameObject.FindGameObjectsWithTag ("Items");
		xmin = ShelfGameManager.xMinPoint;
		xmax = ShelfGameManager.xMaxPoint;
		if (catSpeed > 0) {
			facingLeft = false;
		} 
		else if (catSpeed < 0) {
			facingLeft = true;
		}
		catSpeed = -FitToCameraWidth.publicWidth * screenWidthMultiplier;
		anim.SetBool ("Jump", false);
		jumpTimer = jumpInterval;
		//Debug.Log (catSpeed);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//Moves cat if all conditions are true
		if (catAir == false) {
			if (jumping == false) {
				if (transform.position.y < 10.2f) {
					if (catSpeed > 0) {
						if (transform.position.x < xmax && transform.position.x > 0f) {
							transform.position = new Vector3 (transform.position.x, transform.position.y - 0.01f);
						} else if (transform.position.x > xmin && transform.position.x < 0f) {
							transform.position = new Vector3 (transform.position.x, transform.position.y + 0.01f);
						}
					}
					if (catSpeed < 0) {
						if (transform.position.x < xmax && transform.position.x > 0f) {
							transform.position = new Vector3 (transform.position.x, transform.position.y + 0.01f);
						} else if (transform.position.x > xmin && transform.position.x < 0f) {
							transform.position = new Vector3 (transform.position.x, transform.position.y - 0.01f);
						}
					}
				} 
				else if (transform.position.y < 12.7f && transform.position.y > 12f) {
					if (catSpeed > 0) {
						if (transform.position.x < xmax && transform.position.x > 0f) {
							transform.position = new Vector3 (transform.position.x, transform.position.y - 0.01f);
						} else if (transform.position.x > xmin && transform.position.x < 0f) {
							transform.position = new Vector3 (transform.position.x, transform.position.y + 0.01f);
						}
					}
					if (catSpeed < 0) {
						if (transform.position.x < xmax && transform.position.x > 0f) {
							transform.position = new Vector3 (transform.position.x, transform.position.y + 0.01f);
						} else if (transform.position.x > xmin && transform.position.x < 0f) {
							transform.position = new Vector3 (transform.position.x, transform.position.y - 0.01f);
						}
					}
				}
				transform.position = new Vector3 (transform.position.x + catSpeed * Time.deltaTime, transform.position.y);

			}
		}
		else if (catAir == true) {
			if (rgb2D.velocity.y < 0f && nextShelfPosition >= transform.position.y) {
				anim.SetBool ("Jump", false);
				rgb2D.gravityScale = 0f;
				rgb2D.velocity = Vector2.zero;
				anim.Play ("CatWalk");
				catAir = false;
				jumping = false;
			}
		}

		//Calculate time when cat needs to jump
		if (jumpTimer > 0 && catAir == false) {
			jumpTimer -= Time.deltaTime;
		} 
		else if (jumpTimer <= 0) {
			CatJump ();
			jumpTimer = jumpInterval;
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

	//Checks were all items are and sets values right
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
			jumping = true;
			Invoke ("CatJumpPhys", 1f);
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
			jumping = true;
			Invoke ("CatJumpPhys", 1f);
		} 
		else if (stay > up && stay > down) {
			jumpTimer = jumpInterval;
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
			rgb2D.velocity = new Vector2 (catSpeed, 2f);
			rgb2D.gravityScale = 1f;
			catAir = true;

			//anim.SetBool("Jump",false);
			//anim.Stop();
			//sprite.sprite = jumpSprite;
		} 
		else if (jumpDirectionDown == false) {
			rgb2D.velocity = new Vector2(catSpeed,catJumpHigh);
			rgb2D.gravityScale = 1f;
			catAir = true;
			//anim.SetBool("Jump",false);
			//anim.Stop ();
			//sprite.sprite = jumpSprite;
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
