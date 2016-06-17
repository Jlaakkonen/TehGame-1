using UnityEngine;
using System.Collections;

public class FitToCameraWidth : MonoBehaviour {

	Camera cam;
	float height;
	float width;
	public static float publicWidth;
	float spawnLeft, spawnMiddle = 0f, spawnRight;
	public Transform xMax, xMin;
	public float insetPercentage = 5f;
	public Transform[] leftSpawnpoint, middleSpawnpoint, rightSpawnpoint;
	public Collider2D leftWall, rightWall;

	// Use this for initialization
	void Awake () {
		insetPercentage *= 0.01f;
        Screen.orientation = ScreenOrientation.Portrait;
        cam = Camera.main;
		height = 2f * cam.orthographicSize;
		width = height * cam.aspect;
		xMax.position = new Vector2((width / 2f)-insetPercentage * width,0f);
		xMin.position = new Vector2((width / -2f)+insetPercentage * width,0f);

		//Set values to ShelfgameManagers 
		//This are used in cats movement
		ShelfGameManager.xMaxPoint = xMax.position.x;
		ShelfGameManager.xMinPoint = xMin.position.x;

		//Set value to public variable
		publicWidth = width;

		//Define screens halved half for leftside and rightside
		spawnRight = ((width / 2) - insetPercentage) / 2;
		spawnLeft = -spawnRight;

		//Set gameobject to defined positions
		foreach (var item in rightSpawnpoint) {
			item.position = new Vector2 (spawnRight, item.position.y);
		}
		foreach (var item in leftSpawnpoint) {
			item.position = new Vector2 (spawnLeft, item.position.y);
		}
		foreach (var item in middleSpawnpoint) {
			item.position = new Vector2 (spawnMiddle, item.position.y);
		}

		//Sets borders in screen side
		leftWall.gameObject.transform.position = new Vector2 (-(width / 2f) - ((BoxCollider2D)leftWall).size.x / 2f,leftWall.gameObject.transform.position.y);
		rightWall.gameObject.transform.position = new Vector2 ((width / 2f) + ((BoxCollider2D)rightWall).size.x / 2f,rightWall.gameObject.transform.position.y);
	}
}
