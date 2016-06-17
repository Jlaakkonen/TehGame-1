using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Points : MonoBehaviour {

	static public int breakingPoints = 0;
	static public int breakingLimit;
	public UI uiScript;
	public Text itemText;
	bool GOPOn = false;



	void Start()
	{
		breakingPoints = 0;

		breakingLimit = 2 + (ShelfGameManager.manager.currentLevel + 1) * 2;

		uiScript = FindObjectOfType<UI> ();
	}

	void Update()
	{
		ScoreCalculator ();

		if (breakingPoints == breakingLimit) {
			if (GOPOn == false) {
				uiScript.TextSwitcher (false);
				uiScript.GameOverPanelToggle ();
				GOPOn = true;
			}
		} 
	}

	void ScoreCalculator()
	{
		itemText.text = "Broked Items: " + breakingPoints + "/" + breakingLimit;
	}
}
