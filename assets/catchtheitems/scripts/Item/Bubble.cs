using UnityEngine;
using System.Collections;

public class Bubble : MonoBehaviour {

	Animator anim;


	// Use this for initialization
	void Start()
	{
		anim = GetComponent<Animator> ();

	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Cat") 
		{
			anim.SetBool ("boom", true);


			
		}
	}

	void DestroyObject()
	{
		Destroy (gameObject);
	}
}
