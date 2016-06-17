using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnPoint : MonoBehaviour {

	Collider2D col;

	// Use this for initialization
	void Start () 
	{
		col = gameObject.GetComponent<CircleCollider2D> ();	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//Checks that there are no other items
	public bool CheckSpawn(List<BoxCollider2D> col2D)
	{
		bool touching = false;

		foreach (BoxCollider2D coll in col2D)
		{
			if (col.IsTouching (coll) == true)
			{
				touching = true;
			}
		}

		return touching;
	}
}
