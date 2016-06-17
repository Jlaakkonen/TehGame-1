using UnityEngine;
using System.Collections;

public class SnowflakeManager : MonoBehaviour {

	public GameObject snowflake;
	public Rigidbody2D snowrig;
	private float flakePosition;
	public float cloneTime;
	private int minPosition, maxPosition;
	private float position;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("Snowflake",cloneTime,cloneTime);

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//Instantiats snowflakes at random positions
	void Snowflake()
	{
		flakePosition = Random.Range (ShelfGameManager.xMinPoint, ShelfGameManager.xMaxPoint);
		flakePosition = Mathf.Round (flakePosition);
		Instantiate (snowflake,new Vector2(flakePosition,transform.position.y),transform.rotation);
	}
}
