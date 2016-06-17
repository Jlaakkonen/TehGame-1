using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Collector : MonoBehaviour
{
    bool hasKisse;
    //public string NextLevel;
    int Keys = 0;
    public int Coins = 0;
    bool Finished;
	float torchValue = 1f; 
	float torchAmt = 0f;
	float tVal = 0f;
    public Text Torchshow;
    public Text Keyshow;
    public Text Coinshow;
    public Image Catshow;
    Rigidbody2D rgb2D;
    Collider2D playerColl;
	GameObject plaTorch;
	GameObject EndingDoor;
	Transform lockDoor;
	Torches torches;
	Light torchLight = null;
	bool addLight = false;
	Vector2 allowedDir;


    void Start()
    {
		
		torches = FindObjectOfType<Torches> ();
        rgb2D = GetComponent<Rigidbody2D>();
		playerColl = GetComponentInChildren<CircleCollider2D>();
		plaTorch = GameObject.Find ("TorchLight");
		torchLight = plaTorch.GetComponent<Light> ();
		Debug.Log ("Lock is " + lockDoor);
    }

	void Update()
	{
		
		torchValue = torchValue -0.01f * Time.deltaTime;
		UIhandler.handler.UpdateTorches (torchValue);

		if (torchValue > 1f) {
			torchValue = 1f;
		}
		if (torchValue <= 0f) {
			LabManager.manager.RestartLevel ();
		}
		if (addLight == true) {
			tVal += 1f * Time.deltaTime;
			torchLight.intensity = tVal;
		}
	}
    //Collecting items and !passing through gate to next level! (CHANGE IT!)
    void OnTriggerEnter2D(Collider2D col)
    {
        string tag = col.tag;
		/*
		if (tag == "DropIcicle") 
		{
			Destroy(col.gameObject);
		}
		//Tuleva putoava lumi/jääansa
*/
        if (tag == "Kisse")
        {
			Destroy(GameObject.FindWithTag("Ending Door"));
			Destroy(col.gameObject);
            hasKisse = true;
            UIhandler.handler.showCat();
			//GameObject.FindWithTag ("Ending Door").SetActive (false);

        }
        if (tag == "Key")
        {
            Destroy(col.gameObject);
            Keys++;
            UIhandler.handler.UpdateKeys(Keys);
        }
        if (tag == "Gate")
        {
			Debug.Log (hasKisse);
			if(hasKisse)
			{
				LockdoorMove ();

			}
          // LabManager.manager.CheckForWin(hasKisse);
        }
        if (tag == "Coin")
        {
            Destroy(col.gameObject);
            Coins++;
            UIhandler.handler.UpdateCoins(Coins);
        }
        if (tag == "KeyDoor")
        {
            UseKey(col.GetComponent<KeyGate>());
        }
		if (tag == "TorchCol") {
			GainTorches ();
			Destroy (col.gameObject);
		}
		/*if (tag == "TorchDisCol") {
			if (torches.hasTorch == true) {
				torchLight.intensity = 0f;
				tVal = 0f;
				addLight = false;
				//plaTorch.SetActive (false);
			}
		}*/
        if (tag == "OneWayPass")
        {
			allowedDir = Vector2.zero;
			Vector2 temp = rgb2D.velocity.normalized;
			//Debug.Log ("Rotation = " + col.transform.eulerAngles.z);

			//Debug.Log ("Allowed Direction = " + allowedDir);

			if (col.transform.eulerAngles.z == 0f) 
			{
				allowedDir = Vector2.up;
			}
			else if(col.transform.eulerAngles.z == 90)
			{
				allowedDir = Vector2.left;
			}
			else if(col.transform.eulerAngles.z == 180)
			{
				allowedDir = Vector2.down;
			}
			else if(col.transform.eulerAngles.z == 270)
			{
				allowedDir = Vector2.right;
			}

			int tempY = Mathf.RoundToInt(temp.y);
			int tempX = Mathf.RoundToInt(temp.x);
			int allowedDirY = Mathf.RoundToInt(allowedDir.y);
			int allowedDirX = Mathf.RoundToInt(allowedDir.x);

			if (tempY == allowedDirY && tempX == allowedDirX)
            {
                Physics2D.IgnoreCollision(playerColl, col, true);
            }
        }
    }

    public void OnTriggerStay2D(Collider2D col)
    {
		//string tag = col.tag;
		/*
        if (tag == "OneWayPass")
        {
			Vector2 temp = rgb2D.velocity.normalized;
			Vector2 allowedDir = col.transform.up.normalized;

			int tempY = Mathf.RoundToInt(temp.y);
			int tempX = Mathf.RoundToInt(temp.x);
			int allowedDirY = Mathf.RoundToInt(allowedDir.y);
			int allowedDirX = Mathf.RoundToInt(allowedDir.x);

			if (tempY == allowedDirY && tempX == allowedDirX)
			{
				Physics2D.IgnoreCollision(playerColl, col, true);
			}
        }*/
		/*
		if (col.tag == "TorchDisCol") {
			if (torches.hasTorch == true) {
				//plaTorch.SetActive (false);
				torchLight.intensity = 0f;
				tVal = 0f;
				addLight = false;
			}
			else if (torches.hasTorch == false) {
				//plaTorch.SetActive (true);
				//addLight = true;

				torchLight.intensity = 8f;
			}
		}*/
    }

    public void OnTriggerExit2D(Collider2D col)
    {
        string tag = col.tag;
        if (tag == "OneWayPass")
        {
            Physics2D.IgnoreCollision(playerColl, col, false);
            //coll.GetComponent<OneWayPass>().EnableCollider();
        }
		/*
		if (tag == "TorchDisCol") {
			if (torchLight.intensity == 8f) {
				addLight = true;
			}
			//plaTorch.SetActive (true);
		}*/
    }



    // move to LabManager
    /*void CheckForWin ()
	{
		if(hasKisse && Keys == 2)
		{
			Finished = true;
		}
	}*/
	public float GetTorches()
    {
		return torchValue;
    }
    public void DevourTorches()
    {
		torchAmt = torchValue - 0.1f;
		torchValue = torchAmt;
		UIhandler.handler.UpdateTorches(torchValue);
    }
    public void GainTorches()
    {
		torchAmt = torchValue + 0.1f;
		torchValue = torchAmt;
		UIhandler.handler.UpdateTorches(torchValue);
    }
    public void UseKey(KeyGate KG)
    {
        if (Keys > 0)
        {
            Keys--;
			UIhandler.handler.UpdateKeys(Keys);
            KG.OpenDoor(true);
        }
        else
        {
            KG.OpenDoor(false);
        }
    }

	public void LockdoorMove()
	{
	/*	lockDoor = GameObject.Find("Lock").transform;
		//Debug.Log ("Lock is " + lockDoor);
		Debug.Log ("Lock position " + lockDoor.transform.localPosition.x);
		if(lockDoor.transform.localPosition.x < 0.6f)
		{
			lockDoor.transform.Translate(new Vector3(0.62f,0f,0f));
		}*/
		LabManager.manager.CheckForWin (hasKisse);
	}
}
