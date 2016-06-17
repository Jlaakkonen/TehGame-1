using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameCompleter : MonoBehaviour {

    Button button;

	// Use this for initialization
	void Start ()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(CompleteGame);
	}

    void CompleteGame()
    {
        GlobalGameManager.GGM.CompleteCurrentGame();
    }
}
