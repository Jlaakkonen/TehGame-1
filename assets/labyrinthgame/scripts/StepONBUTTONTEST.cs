using UnityEngine;
using System.Collections;

public class StepONBUTTONTEST : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnCollisionEnter2D (Collision2D coll)
	{
		//Check collision name
		Debug.Log("collision name = " + coll.gameObject.name);
		if(coll.gameObject.name == "ButtonBOOM")
		{
			Destroy(coll.gameObject);
		}
	}
}
