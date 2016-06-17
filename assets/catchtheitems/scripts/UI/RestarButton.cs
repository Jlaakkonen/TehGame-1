using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RestarButton : MonoBehaviour {

	Button restart;


	// Use this for initialization
	void Start () {
		restart = GetComponent<Button> ();
		restart.onClick.AddListener (Restart);
	}
	
	void Restart()
	{
		ShelfGameManager.manager.RestartLevel ();
	}

}
