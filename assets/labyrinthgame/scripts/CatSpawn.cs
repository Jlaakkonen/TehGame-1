using UnityEngine;
using System.Collections;

public class CatSpawn : MonoBehaviour {

	GameObject[] spawnpoints;
	int spawn;

	// Use this for initialization
	void Start () {
		spawnpoints = GameObject.FindGameObjectsWithTag ("CatSpawn");
		//Debug.Log (spawnpoints.Length);
		CatSpawner ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void CatSpawner()
	{
		spawn = Random.Range(1,spawnpoints.Length);

		transform.position = new Vector3 (spawnpoints [spawn].transform.position.x, spawnpoints [spawn].transform.position.y);
	}
}
