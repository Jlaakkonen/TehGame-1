using UnityEngine;
using System.Collections;

public class BlockRot : MonoBehaviour {

	public float RotTime = 100f;
	float Timer = 0;
	
	void Update ()
	{
		Timer += Time.deltaTime;
		if (Timer >= RotTime)
		{
			Timer = 0;
			RotateBlock ();
		}
	}
	void RotateBlock ()
	{
		transform.Rotate (Vector3.forward * -90);
	}
}
