using UnityEngine;
using System.Collections;

public class ButtonMove : MonoBehaviour {

	public float speed = 10f;
	private SpriteRenderer spriteRenderer;
	public Sprite spriteCenter;
	public Sprite spriteLeft;
	public Sprite spriteRight;

	// Use this for initialization
	void Start () 
	{
		spriteRenderer = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		Move ();
	}

	void Move()
	{
		if (Input.GetKey ("left"))
		{
			transform.Translate (-speed * Time.deltaTime,0 ,0);
			spriteRenderer.sprite = spriteLeft;
		}

		else if (Input.GetKey ("right")) 
		{
			transform.Translate (speed * Time.deltaTime, 0, 0);
			spriteRenderer.sprite = spriteRight;
		} 

		else 
		{
			spriteRenderer.sprite = spriteCenter;
		}
	}
		
}
