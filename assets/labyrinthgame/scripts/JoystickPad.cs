using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class JoystickPad : MonoBehaviour {

	//napin position
	//siirretään nappi hiiren kohdalle
	//jos hiiri menee napin alueen yli = nappi saa maksimi alueen arvon
	//pelaaja liikkuu napin osoittamaan suuntaan

	CharacterMove  player; 
	Vector2 playerForce;
	float insetX,insetY;
	public float maxRange = 150f;
	public float minRange = 20f;
	Camera cam;
	GUITexture gText;
	GUITexture gTextPad;
	float insetXStart,insetYStart;
	float insetWidth, insetHeight, insetYRepair;
	float maxInsetRadius;
	Vector2 startVec;
	Vector2 radius;



	void Start()
	{
		
		player = FindObjectOfType<CharacterMove> ();
		cam = FindObjectOfType<Camera> ();
		gText = GetComponent<GUITexture> ();
		gTextPad = transform.Find ("JoystickPad").GetComponent<GUITexture> ();
		insetYStart = cam.pixelHeight/4;
		insetXStart = cam.pixelWidth/4;
		insetWidth = insetYStart;
		insetHeight = insetYStart / 4;
		insetYRepair = insetHeight / 2;

		startVec = new Vector2 (insetXStart, insetYStart - insetYRepair);
		gText.pixelInset = new Rect(insetXStart, insetYStart - insetYRepair, 0f, insetHeight);
		gTextPad.pixelInset = new Rect(insetXStart - insetWidth / 2f,insetYStart - insetWidth / 2f,insetWidth,insetWidth);
		//gTextPad.pixelInset = new Rect(insetXStart + insetYRepair, insetYStart - insetYRepair, -insetYRepair*2, insetHeight);
		//gTextPad.transform.localScale = new Vector3 (2,2);
	}

	void Update()
	{
        /*if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
		{
			insetY = Input.GetTouch(0).position.y - insetYStart;
			insetX = Input.GetTouch(0).position.x - insetXStart;
			radius = new Vector2(insetX,insetY);
			gText.pixelInset = new Rect (radius.x + insetXStart,radius.y + insetYStart,0f, insetHeight);
			if (radius.magnitude <= maxRange && radius.magnitude > minRange) {
				gText.pixelInset = new Rect (radius.x + insetXStart, radius.y + insetYStart - insetYRepair, 0f, insetHeight);
				//player.ReceiveInput (CalculateForce (radius));
				player.forceVector = CalculateForce(radius);
			}
			else if (radius.magnitude > maxRange) {
				radius.Normalize ();
				gText.pixelInset = new Rect (radius * maxRange + startVec, new Vector2 (0f, insetWidth));
				player.forceVector = radius;
				//player.ReceiveInput(CalculateForce(radius));
			} 
			else {
				player.forceVector = new Vector2 (0f, 0f);
			}
		}
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Stationary)
		{
			insetY = Input.GetTouch(0).position.y - insetYStart;
			insetX = Input.GetTouch(0).position.x - insetXStart;
			Vector2 radius = new Vector2(insetX,insetY);
			gText.pixelInset = new Rect (radius.x + insetXStart,radius.y + insetYStart - insetYRepair,0f,insetHeight);
			if (radius.magnitude <= maxRange && radius.magnitude > minRange) {
				gText.pixelInset = new Rect (radius.x + insetXStart, radius.y + insetYStart - insetYRepair, 0f, insetHeight);
				//player.ReceiveInput (CalculateForce (radius));
				player.forceVector = CalculateForce(radius);
			}
			else if (radius.magnitude > maxRange) {
				radius.Normalize ();
				gText.pixelInset = new Rect (radius * maxRange + startVec, new Vector2 (0f, 0f));
				player.forceVector = radius;
				//player.ReceiveInput(CalculateForce(radius));
			} 
			else {
				player.forceVector = new Vector2 (0f, 0f);
			}
		}
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
		{
			player.forceVector = new Vector2 (0f, 0f);
			gText.pixelInset = new Rect (startVec, new Vector2 (0f, insetWidth));
		}*/
        
        player.forceVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

    }

    void OnMouseDrag()
	{
		insetY = Input.mousePosition.y - insetYStart;
		insetX = Input.mousePosition.x - insetXStart;
		Vector2 radius = new Vector2(insetX,insetY);
		gText.pixelInset = new Rect (radius.x + insetXStart,radius.y + insetYStart - insetYRepair,0f,insetHeight);
		if (radius.magnitude <= maxRange && radius.magnitude > minRange) {
			gText.pixelInset = new Rect (radius.x + insetXStart, radius.y + insetYStart - insetYRepair, 0f, insetHeight);
			//player.ReceiveInput (CalculateForce (radius));
			player.forceVector = CalculateForce(radius);
		}
		else if (radius.magnitude > maxRange) {
			radius.Normalize ();
			gText.pixelInset = new Rect (radius * maxRange + startVec, new Vector2 (0f, insetHeight));
			player.forceVector = radius;
			//player.ReceiveInput(CalculateForce(radius));
		} 
		else {
			player.forceVector = new Vector2 (0f, 0f);
		}
	}

	void OnMouseUp()
	{
		player.forceVector = new Vector2 (0f,0f);
		gText.pixelInset =  new Rect(insetXStart, insetYStart - insetYRepair, 0f, insetHeight);
		//gText.pixelInset =  new Rect(0f,0f,0f,0f);
	}


	Vector2  CalculateForce(Vector2 vec)
	{
		float radMag = vec.magnitude;
		float percent = Mathf.Clamp01 (radMag / maxRange);
		vec.Normalize ();
		vec *= percent;
		return vec;
	}
}
