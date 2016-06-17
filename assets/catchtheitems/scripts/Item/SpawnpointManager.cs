using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnpointManager : MonoBehaviour {
	
	public float spawnTime = 3f;            
	private SpawnPoint[] spawnPoints;
	private int spawnPointIndex;
	private GameObject[] cat;
	private List<BoxCollider2D> catCollider;
	public GameObject[] listDroppable;
	private List<BoxCollider2D> listBubbleCollider;
	static public GameObject listItem;
	private GameObject[] sceneItems;
	bool ableToSpawn = false;
	bool spawn = false;

	// Use this for initialization
	void Start () 
	{
		
		InvokeRepeating ("Spawn", spawnTime, spawnTime);

		spawnPoints = FindObjectsOfType<SpawnPoint> ();

		cat = GameObject.FindGameObjectsWithTag ("Cat");
		catCollider = new List<BoxCollider2D>();
		foreach (GameObject itemC in cat) {
			catCollider.Add(itemC.GetComponent<BoxCollider2D>());
		}

		listBubbleCollider = new List<BoxCollider2D> ();
	}

	//Checks that there is not other items and Instatiats one more item
	void Spawn ()
	{

		listItem = listDroppable[Random.Range(0,listDroppable.Length)];

		ableToSpawn = false;



		if (TimerScript.seconds <= 0) 
		{
			
			//Destroy(GameObject.FindGameObjectWithTag("Items"));
			return;
		} 
		else if (Points.breakingPoints >= Points.breakingLimit) 
		{
			
			//Destroy(GameObject.FindGameObjectWithTag("Items"));
			return;
		}

		else 
		{
			//Debug.Log ();

			do {
				spawnPointIndex = Random.Range (0, spawnPoints.Length);



				if (spawnPoints [spawnPointIndex].CheckSpawn (catCollider) == false) 
				{
					
					if (spawnPoints [spawnPointIndex].CheckSpawn(listBubbleCollider) == false) 
					{
						ableToSpawn = true;
						spawn = true;

					}
					else 
					{
						ableToSpawn = true;
						spawn = false;
					}

				}
			} 
			while (ableToSpawn != true);
	
			if(spawn == true)
			{
				Instantiate (listItem, spawnPoints [spawnPointIndex].transform.position, spawnPoints [spawnPointIndex].transform.rotation);
			}

			ItemListManager();
		}

	}

	//Keeps list of spawned items
	public void ItemListManager()
	{
		sceneItems = GameObject.FindGameObjectsWithTag ("Items");

		foreach (GameObject item in sceneItems) {
			listBubbleCollider.Add(item.GetComponent<BoxCollider2D>());
		}
	}
		
}
	