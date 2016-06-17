using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterMove : MonoBehaviour {
	
	public float playerSpeed = 10f;
    Vector2 directionVector;
    Rigidbody2D rgb;
    SpriteRenderer ebens;
    Animator anim;
    Collider2D playerCol;
	public Vector2 forceVector;
    
    void Start()
    {
        rgb = GetComponent<Rigidbody2D>();
        ebens = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        playerCol = GetComponentInChildren<CircleCollider2D>();
		forceVector = new Vector2 (0f, 0f);
    }
	void Update () 
	{
		MoveForward();
		ReceiveInput (forceVector);
		AnimationControl ();
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if(playerCol.enabled == true)
            {
                playerCol.enabled = false;
            }
            else
            {
                playerCol.enabled = true;
            }
        }
	}

    public void ReceiveInput(Vector2 pointVector)
    {
		directionVector = pointVector * playerSpeed;
		rgb.velocity = directionVector;
		//Debug.Log ("Direction vector: "+ directionVector);
		//Debug.Log ("RGB2D velocity: " + rgb.velocity);
		//Debug.Log ("Pelaaja liikkui!");
    }

	void AnimationControl()
	{
		//Debug.Log ("VelocityX: " + rgb.velocity.x);
		//Debug.Log ("VelocityY: " + rgb.velocity.y);

		if (rgb.velocity.y >= 0.5f && rgb.velocity.x < 1f && rgb.velocity.x > -1f) {
			anim.SetBool ("GoUp", true);
			anim.SetBool ("GoDown", false);
			anim.SetBool ("GoRight", false);
			anim.SetBool ("GoLeft", false);
		} 
		else if (rgb.velocity.y <= -0.5f && rgb.velocity.x < 1f && rgb.velocity.x > -1f) {
			anim.SetBool ("GoUp", false);
			anim.SetBool ("GoDown", true);
			anim.SetBool ("GoRight", false);
			anim.SetBool ("GoLeft", false);
		} 
		else if (rgb.velocity.x >= 0.6f) {
			anim.SetBool ("GoUp", false);
			anim.SetBool ("GoDown", false);
			anim.SetBool ("GoRight", true);
			anim.SetBool ("GoLeft", false);
		} 
		else if (rgb.velocity.x <= -0.6f) {
			anim.SetBool ("GoUp", false);
			anim.SetBool ("GoDown", false);
			anim.SetBool ("GoRight", false);
			anim.SetBool ("GoLeft", true);
		} 
		else {
			anim.SetBool ("GoUp", false);
			anim.SetBool ("GoDown", false);
			anim.SetBool ("GoRight", false);
			anim.SetBool ("GoLeft", false);
		}
	}

	void MoveForward()
	{
        //Vector components
        float x1 = 0f, x2 = 0f, y1 = 0f, y2 = 0f;

        //SetAnimations
        if (Input.GetButtonDown("Up")||Input.GetButton("Up"))
        {
            anim.SetBool("GoUp", true);
            anim.SetBool("GoDown", false);
            anim.SetBool("GoRight", false);
            anim.SetBool("GoLeft", false);
            y1 = 1f;
        }
        if (Input.GetButtonDown("Down") || Input.GetButton("Down"))
        {
            anim.SetBool("GoDown", true);
            anim.SetBool("GoUp", false);
            anim.SetBool("GoRight", false);
            anim.SetBool("GoLeft", false);
            y2 = -1f;
        }
        if (Input.GetButtonDown("Right") || Input.GetButton("Right"))
        {
            anim.SetBool("GoRight", true);
            anim.SetBool("GoUp", false);
            anim.SetBool("GoDown", false);
            anim.SetBool("GoLeft", false);
            x1 = 1f;
        }
        if (Input.GetButtonDown("Left") || Input.GetButton("Left"))
        {
            anim.SetBool("GoLeft", true);
            anim.SetBool("GoUp", false);
            anim.SetBool("GoDown", false);
            anim.SetBool("GoRight", false);
            x2 = -1f;
        }
        //Cancel move animations
        if (Input.GetButtonUp("Up"))
        {
            anim.SetBool("GoUp", false);
            y1 = 0f;
        }
        if (Input.GetButtonUp("Down"))
        {
            anim.SetBool("GoDown", false);
            y2 = 0f;
        }
        if (Input.GetButtonUp("Right"))
        {
            anim.SetBool("GoRight", false);
            x1 = 0f;
        }
        if (Input.GetButtonUp("Left"))
        {
            anim.SetBool("GoLeft", false);
            x2 = 0f;
        }
        //Calculate directional vector
        Vector2 temp = new Vector2(x1 + x2, y1 + y2);
        temp.Normalize();
        directionVector = temp * playerSpeed;
        rgb.velocity = directionVector;
    }

    public Vector2 GetDirectionVector()
    {
        return directionVector;
    }

    void ChangeDirection(Vector2 dir)
    {
        /*float xScale = 0.5f;
        float yScale = 0.5f;
        if(dir.y > 0)
        {
            anim.SetBool("GoUp", true);
        }
        else if(dir.y < 0)
        {
            anim.SetBool("GoDown", true);
        }
        if (dir.x > 0)
        {
            anim.SetBool("GoRight", true);
        }
        else if (dir.x < 0)
        {
            anim.SetBool("GoLeft", true);
        }
        else if(dir.magnitude == 0)
        {
            anim.SetBool("GoUp", false);
            anim.SetBool("GoDown", false);
            anim.SetBool("GoRight", false);
            anim.SetBool("GoLeft", false);
        }
        */
        //gameObject.transform.localScale = new Vector3(xScale, yScale, 1f);
    }
}