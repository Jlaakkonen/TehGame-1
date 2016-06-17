using UnityEngine;
using System.Collections;

public class ItemHolder : MonoBehaviour {

	Animator anim;
	private DropItem drop;
	private PowerUpSpeedScript dropSpeed;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		drop = GetComponentInChildren<DropItem> ();
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Cat") 
		{
			drop.WaitActivate ();
			anim.SetBool ("boom", true);
		}
			
	}

	void DestroyObject()
	{
		Destroy (gameObject);
	}
}
