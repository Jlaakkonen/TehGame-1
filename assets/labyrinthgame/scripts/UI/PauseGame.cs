using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour {

    public GameObject Pauseimg;

    public void OnButtonClick ()
    {
        Pauseimg.SetActive(true);
        Time.timeScale = 0;
    }
}
