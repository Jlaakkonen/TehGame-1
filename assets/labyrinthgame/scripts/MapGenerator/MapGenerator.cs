using UnityEngine;
using System.Collections;

public class MapGenerator : MonoBehaviour {

	int level = 1;
	public GameObject tilefloorPref;
	public GameObject tilewallPref;
	public GameObject exitdoorPref;
	public GameObject keydoorPref;
	public GameObject keyPref;
	public GameObject coinPref;
	public GameObject catPref;
	public GameObject torchCollectablePref;
	GameObject player;

	string[] mapData;

	int thisMapSizeX;
	int thisMapSizeY;

	void Start()
	{
		player = GameObject.Find ("Player");

		level = LabManager.manager.currentLevel;

		if (level == 0) {


			string[] mapData1 = 
			{
				"wwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwww",
				"wffffffffffffffffffwwwwwwfcccwwwww",
				"wfpffffffffffffwwwffffffffwwcwwwww",
				"wfffffffffffwffwwwwwwwwwwwwwkwwwww",
				"wwwwwwwwwwwwwffwwwwwwwwwwwwwwwwwww",
				"wwwwwwwwwwwwwffdfffffffffffffffwww",
				"wwcccwwwwwwffffwwwwwwwwwwwwwwffwww",
				"wwcacffffffffffwwwwwwwwwwwwwwffwww",
				"wwcccwwwwwwwwwwwwcccwwwwwwwwwfffww",
				"wwwwwwwwwwwwwwwffffffffwwwwwwfffww",
				"wwwwwwwwwwwwwwwfwwwwwwfffffffffffw",
				"wwwwwwwwwwwwwwwEwwwwwwwwwwwwwwwwww"
			};

			thisMapSizeX = mapData1[0].Length;
			thisMapSizeY = mapData1.Length;

			mapData = mapData1;
		}

		else if (level == 1) {


			string[] mapData2 = 
			{
				"wwwwwwwwwwwwwwwwwwww",
				"wwwwfcccfwwwwwwwwwww",
				"wpfffwwwffffcdfffffE",
				"wwwwfcccfwwwfwwwwwww",
				"wwwwwwwwwwwwfwwwwwww",
				"wwwwfffffffftffwwwww",
				"wwwwfwwwwwwwwwfwwwww",
				"wwwwfwwwwwwwfffwwwww",
				"wwfffwwwwwwwfwwwwwww",
				"wwfafwwwwwccffffwwww",
				"wwcccwwwwwwwwwwkwwww",
				"wwwwwwwwwwwwwwwwwwww"
			};

			thisMapSizeX = mapData2[0].Length;
			thisMapSizeY = mapData2.Length;

			mapData = mapData2;
		}

		InstantiateMap ();
	}

	void InstantiateMap()
	{
		for (int x = 0; x < thisMapSizeX; x++) {
			for (int y = 0; y < thisMapSizeY; y++) {
				
				GameObject nextTile = null;
				Vector3 tilePosition = new Vector3(x,-y);

				if(mapData[y][x] == 'f')
				{
					nextTile = tilefloorPref;
					nextTile = (GameObject)Instantiate(nextTile,tilePosition,Quaternion.identity);
					nextTile.transform.SetParent (transform);
				}
				else if(mapData[y][x] == 'w')
				{
					nextTile = tilewallPref;
					nextTile = (GameObject)Instantiate(nextTile,tilePosition,Quaternion.identity);
					nextTile.transform.SetParent (transform);
				}
				else if(mapData[y][x] == 'p')
				{
					nextTile = tilefloorPref;
					nextTile = (GameObject)Instantiate(nextTile,tilePosition,Quaternion.identity);
					nextTile.transform.SetParent (transform);
					player.transform.position = tilePosition;
				}
				else if(mapData[y][x] == ' ')
				{
					nextTile = null;
				}
				else if(mapData[y][x] == 'k')
				{
					nextTile = tilefloorPref;
					nextTile = (GameObject)Instantiate(nextTile,tilePosition,Quaternion.identity);
					nextTile.transform.SetParent (transform);
					Instantiate (keyPref, tilePosition, Quaternion.identity);
				}
				else if(mapData[y][x] == 'd')
				{
					nextTile = tilefloorPref;
					nextTile = (GameObject)Instantiate(nextTile,tilePosition,Quaternion.identity);
					nextTile.transform.SetParent (transform);
					Instantiate(keydoorPref,tilePosition,Quaternion.identity);
				}
				else if(mapData[y][x] == 'E')
				{
					nextTile = exitdoorPref;
					Instantiate(nextTile,tilePosition,Quaternion.identity);
				}
				else if(mapData[y][x] == 'c')
				{
					nextTile = tilefloorPref;
					nextTile = (GameObject)Instantiate(nextTile,tilePosition,Quaternion.identity);
					nextTile.transform.SetParent (transform);
					Instantiate(coinPref,tilePosition,Quaternion.identity);
				}
				else if(mapData[y][x] == 'a')
				{
					nextTile = tilefloorPref;
					nextTile = (GameObject)Instantiate(nextTile,tilePosition,Quaternion.identity);
					nextTile.transform.SetParent (transform);
					Instantiate(catPref,tilePosition,Quaternion.identity);
				}

				else if(mapData[y][x] == 't')
				{
					nextTile = tilefloorPref;
					nextTile = (GameObject)Instantiate(nextTile,tilePosition,Quaternion.identity);
					nextTile.transform.SetParent (transform);
					Instantiate(torchCollectablePref,tilePosition,Quaternion.identity);
				}
			}
		}
	}
}
