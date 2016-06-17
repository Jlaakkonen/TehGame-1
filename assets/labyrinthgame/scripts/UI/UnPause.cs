using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UnPause : MonoBehaviour {

    public GameObject Pauseimg;

    public void OnButtonClick ()
    {
        Time.timeScale = 1;
        //Redo to UI function
        Pauseimg.SetActive(false);
    }
}
