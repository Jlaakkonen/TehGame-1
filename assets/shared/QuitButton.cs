using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class QuitButton : MonoBehaviour
{

    Button butt;
        
	// Use this for initialization
	void Start ()
    {
        butt = GetComponent<Button>();
        butt.onClick.AddListener(QuitLife);
	}

    void QuitLife()
    {
        Application.Quit();
    }
}
