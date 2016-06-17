using UnityEngine;
using System.Collections;

public class PlayerPushBlock : MonoBehaviour
{
    Rigidbody2D rgb;
    CharacterMove moveScript;
    bool checking;
    float timer;
    public float timerThreshold;
    public float rayLength;
    Vector2 relativeVector;
    Vector2 pushVector;
    Transform BlockTransform;
    //public GameObject testBlock;
    public float VectorThreshold;
	RaycastHit2D rayHit;
	Vector2 rayStartPoint;

    void Start()
    {
        rgb = GetComponentInParent<Rigidbody2D>();
        moveScript = GetComponentInParent<CharacterMove>();
    }

    void Update()
    {
        //If touching a block, and player is movin against it
        if (checking && moveScript.GetDirectionVector().magnitude > 0)
        {
            timer += Time.deltaTime;
        }
        //We dont want to push the block if the player isn't walking towards it for the duration of the timer
        else
        {
            timer = 0;
        }
        if (timer >= timerThreshold && rgb.velocity.magnitude > 0)
        {
            //Reset variables
            timer = 0;
            relativeVector = Vector2.zero;
            pushVector = Vector2.zero;
            //Make the vector that goes from the player to the block
			relativeVector = (Vector2)BlockTransform.position - (Vector2)transform.position;         
			Debug.Log ("Relative Vector: " + relativeVector);
            //Check which vector component is greater and make sure that the other is within certain bounds
            /*if (relativeVector.y < relativeVector.x && relativeVector.x < VectorThreshold && relativeVector.x > -VectorThreshold)
            {
                //Pushing the block down
                pushVector = new Vector2(0f, -1f);
				PushBlock();
				Debug.Log ("Down");
            }
            else if (relativeVector.x > relativeVector.y && relativeVector.y < VectorThreshold && relativeVector.y > -VectorThreshold)
            {
                //Pushing the block right
                pushVector = new Vector2(1f, 0f);
				PushBlock();
				Debug.Log ("Right");
            }
            else if (relativeVector.y > relativeVector.x && relativeVector.x < VectorThreshold && relativeVector.x > -VectorThreshold)
            {
                //Pushing the block up
                pushVector = new Vector2(0f, 1f);
				PushBlock();
				Debug.Log ("Up");
            }
            else if (relativeVector.x < relativeVector.y && relativeVector.y < VectorThreshold && relativeVector.y > -VectorThreshold)
            {
                //Pushing the block left
                pushVector = new Vector2(-1f, 0f);
				PushBlock();
				Debug.Log ("Left");
            }*/
			if (relativeVector.x > 0.55f) {
				pushVector = new Vector2 (1f, 0f);
				PushBlock ();
				//rayStartPoint = BlockTransform.transform.position + pushVector;
				Debug.Log ("Right");
			} 
			else if (relativeVector.x < -0.55f) {
				pushVector = new Vector2 (-1f, 0f);
				PushBlock ();
				Debug.Log ("Left");
			} 
			else if (relativeVector.y > 0.55f) {
				pushVector = new Vector2 (0f, 1f);

				PushBlock ();
				Debug.Log ("Up");
			} 
			else if (relativeVector.y < -0.55) {
				pushVector = new Vector2 (0f, -1f);
				PushBlock ();
				Debug.Log ("Down");
			} 
			else {
				Debug.Log ("Error!");
			}
            //Check for obstruction (walls, or anything with a collider) with raycast
            //RaycastHit2D rayHit = Physics2D.Raycast(BlockTransform.position, pushVector, rayLength);
            //Debug.DrawRay(BlockTransform.position, pushVector, Color.yellow, rayLength);
			//Debug.Log("RayHitCollider: " + rayHit.collider);
            //if (rayHit.collider == null)
            //{
                //If nothing found, push the block
                //PushBlock();
            //}            
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        //Pushable block lost
        if (collision.gameObject.CompareTag("PushBlock"))
        {
            checking = false;
			Debug.Log ("Collision undetectet");
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        //Pushable block found
        if (collision.gameObject.CompareTag("PushBlock"))
        {
            checking = true;
            BlockTransform = collision.transform;
			//Debug.DrawRay(BlockTransform.position, pushVector, Color.blue, 1.2f);
			Debug.Log ("Collision detectet");
        }
    }
    void PushBlock()
    {
		rayStartPoint = (Vector2)BlockTransform.transform.position + pushVector;
		rayHit = Physics2D.Raycast (rayStartPoint, pushVector,rayLength);
		Debug.Log ("RaycastHit = " + rayHit.collider);
		//Check that there is no other block or collectable pushed direction
		if(rayHit.collider == null)
		{
        	//Pushing the block along the vector from the player towards the block, rounded to 90 degree angle
        	BlockTransform.Translate(pushVector);
		}
    }
}
