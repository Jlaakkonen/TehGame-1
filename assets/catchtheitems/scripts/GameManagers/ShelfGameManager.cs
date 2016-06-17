using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShelfGameManager : GameManager
{
	//public static new ShelfManager manager;
	bool levelComplete;
	public static Transform destroyPoint;
	public static float xMinPoint, xMaxPoint;
	public static bool gyroOn = false;
	//int gyroValue = 0;
	Transform desPoint;
	private Toggle gyroButton;
	//Toggle gyroToggle;


	//Singleton check
	protected override void Awake()
	{
		if (manager == null)
		{
			DontDestroyOnLoad(gameObject);
			manager = this;
		}
		else if (manager != this)
		{
			Destroy(gameObject);
			Debug.Log ("Destroy ShelfManager");
		}
	}

	public override void PlayerWin()
	{
		if (manager.completedLevels.Contains(currentLevel) == false)
		{
			completedLevels.Add(currentLevel);
		}
	}
	public override void PlayerLose()
	{
		base.PlayerLose();
	}
	public override void CheckForWin(bool hasKisse)
	{
		if (hasKisse)
		{
			PlayerWin();
		}
	}

	public override void RestartLevel()
	{
		//Application.LoadLevel (Application.loadedLevel);
		base.RestartLevel();
		Time.timeScale = 1f;
	}

	void OnLevelWasLoaded()
	{
		Time.timeScale = 1f;
	}
	public void LoadNextLevel()
	{
		//base.LoadNextLevel ();
		LoadLevel(currentLevel + 1);
	}

	public void GyroToggle()
	{
		gyroButton = GameObject.Find ("GyroToggle").GetComponent<Toggle> ();

		if (gyroButton.isOn == true) 
		{
			gyroOn = true;
			Debug.Log ("Gyro Päällä");
		} 
		else if (gyroButton.isOn == false) 
		{
			gyroOn = false;
			Debug.Log ("Gyro Poissa Päältä");
		}
	}
	public bool ReturnToggle()
	{
		return gyroOn;
	}
}