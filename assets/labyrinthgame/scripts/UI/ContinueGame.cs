using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ContinueGame : MonoBehaviour {

    public Image Pauseimg;

    void OnButtonClick ()
    {
        Time.timeScale = 1;
        Pauseimg.enabled = false;
    }
}
