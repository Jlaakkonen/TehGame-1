using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour {

	public GameObject gameOverPanel;
	public GameObject pausePanel;
	public GameObject tutorialPanel;
	public Text endText;
	bool GOPanel = false;


	// Use this for initialization
	void Start () {
		gameOverPanel.SetActive(GOPanel);
		pausePanel.SetActive (false);

		if (ShelfGameManager.manager.currentLevel == 0) {
			tutorialPanel.SetActive (true);
			Time.timeScale = 0f;
		}
	}

	public void GameOverPanelToggle()
	{
		if (GOPanel == true) {
			GOPanel = false;
			gameOverPanel.SetActive (false);
			Time.timeScale = 1f;
		}
		else {
			GOPanel = true;
			gameOverPanel.SetActive (true);
			Time.timeScale = 0f;
		}

	}

	public void Pause()
	{
		Time.timeScale = 0f;
		pausePanel.SetActive (true);
	}

	public void Resume()
	{
		Time.timeScale = 1f;
		pausePanel.SetActive (false);
		tutorialPanel.SetActive (false);
	}

	public void Levelselect()
	{
		ShelfGameManager.manager.GoToMenu ();
	}

	//Switches text in gameoverpanel
	public void TextSwitcher(bool won)
	{
		if (won == false) {
			endText.text = "Too bad." + "\n" + "You can try again.";
		} 
		else if (won == true) {
			endText.text = "Congratulation." + "\n" + "You can move to next level.";
		}
	}
}
