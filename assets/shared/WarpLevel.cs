using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WarpLevel : MonoBehaviour
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
        GameManager.manager.PlayerWin();
    }
}
