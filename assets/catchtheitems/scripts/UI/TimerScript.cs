using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimerScript : MonoBehaviour {

	public Text timer;
	float timeLeft;
	float addTimeValue;
	public static int seconds;
	private UI sceneUi;
	bool GOPOn = false;
	public float time = 30f;

	// Use this for initialization
	void Start () {
		sceneUi = FindObjectOfType<UI> ();
		//Debug.Log (ShelfGameManager.manager.currentLevel);
		addTimeValue = ShelfGameManager.manager.currentLevel * 10f;
		time = time + addTimeValue;
		timeLeft = time;
		//timeLeft = (time * (ShelfGameManager.manager.currentLevel + 1))/2f;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (timeLeft > 0) {
			timeLeft -= Time.deltaTime;
			seconds = (int)timeLeft;
			timer.text = "Time: " + seconds;
		} 
		else if (timeLeft <= 0) {
			if (GOPOn == false) {
				sceneUi.GameOverPanelToggle ();
				sceneUi.TextSwitcher (true);
				ShelfGameManager.manager.PlayerWin ();
				GOPOn = true;
			}
		}
	}

}
