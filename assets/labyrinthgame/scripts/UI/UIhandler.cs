using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIhandler : MonoBehaviour
{
    public static UIhandler handler;
    public Image pauseMenu;
    public Text keyAmt;
    public Text torchAmt;
	public Image torchImg;
    public Text coinAmt;
    public Image catImage;
	Collector collScript;
	float torchFill;
	float startTorch = 1f;
	float keyNum;

	void Start ()
    {
        /*if (handler == null)
        {
            DontDestroyOnLoad(gameObject);
            handler = this;
        }
        else if (handler != this)
        {
            Destroy(gameObject);
        }*/
		collScript = FindObjectOfType<Collector> ();
        handler = this;
		UpdateTorches (startTorch);
    }

    public void ShowPause ()
    {
        pauseMenu.gameObject.SetActive(true);
    }
    public void HidePause()
    {
        pauseMenu.gameObject.SetActive(false);
    }
    public void showCat ()
    {
        catImage.gameObject.SetActive(true);
    }
    public void UpdateCoins (int CoinAmt)
    {
        coinAmt.text = CoinAmt.ToString();
    }
	public void UpdateTorches(float TorchAmt)
    {
		torchFill = TorchAmt;
		torchImg.fillAmount = torchFill;
        torchAmt.text = "Torches: " + TorchAmt.ToString() + "/" /* + maxTorches*/;

    }
    public void UpdateKeys(int KeyAmt)
    {
        keyAmt.text = KeyAmt.ToString();
    }
    public void GoToMenu()
    {
        LabManager.manager.GoToMenu();
        Time.timeScale = 1f;
    }
}
