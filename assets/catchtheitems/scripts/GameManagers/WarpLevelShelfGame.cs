using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WarpLevelShelfGame : MonoBehaviour
{
    Button button;

    //Temporal skip button
	void Start ()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(WarpToNext);
    }
    void WarpToNext ()
    {
		((ShelfGameManager)GameManager.manager).PlayerWin ();
		((ShelfGameManager)GameManager.manager).LoadNextLevel ();
    }
}
