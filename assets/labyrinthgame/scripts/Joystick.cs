using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Joystick : MonoBehaviour
{
    //Vector3 initialPress;
    Vector3 currentPosition;
	//Vector3 offset;
    //Vector3 pointVector;
    Vector3 pointVector;
    Vector3 imgStartPosition;
    public float inputDeadzone;
    public float maxInput = 100f;
    //public Sprite movving;
    //public Sprite notMoccing;
    Image joystickimg;
    GameObject joystick;
    RectTransform ImgTransform;
    CharacterMove player;

    //public Text temp2;
    //public Text raw2;
    //public Text point2;

    void Start()
    {
        joystick = gameObject.transform.Find("Joystick").gameObject;
		joystickimg = joystick.GetComponent<Image>();
        player = FindObjectOfType<CharacterMove>();
        //nuppi = GetComponent<RectTransform>();
        //current.color = Color.clear;
        imgStartPosition = transform.position;
        //Debug.Log(player);
        //Debug.Log(imgStartPosition);
    }

    void Update()
    {
        Vector2 tempppp = pointVector;
        tempppp.Normalize();
        //magni.text = pointVector2.ToString();
        
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            currentPosition = Input.GetTouch(0).position;
            //initialPress = Input.GetTouch(0).position;
            //imgTransform.position = initialPress;
            //nuppi.color = Color.white;
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            pointVector = Input.mousePosition - imgStartPosition;
            //imgTransform.position = pointVector;
            if (pointVector.magnitude <= maxInput)
            {
                //p
                //pointVector2 = Input.mousePosition - initialPress;
                if (pointVector.magnitude > inputDeadzone)
                {
                    player.ReceiveInput(CalculateInput(pointVector));
					joystickimg.transform.position = Input.mousePosition;
                }
            }
            else
            {
                player.ReceiveInput(CalculateInput(pointVector));
                pointVector.Normalize();
				joystickimg.transform.position = (pointVector * maxInput) + transform.position;
            }
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Stationary)
        {
            pointVector = Input.mousePosition - imgStartPosition;
            if (pointVector.magnitude <= maxInput)
            {
                //pointVector = Input.mousePosition - initialPress;
                //imgTransform.position = initialPress;
                if (pointVector.magnitude > inputDeadzone)
                {
                    player.ReceiveInput(CalculateInput(pointVector));
					joystickimg.transform.position = Input.mousePosition;
                }
            }
            else
            {
                player.ReceiveInput(CalculateInput(pointVector));
                pointVector.Normalize();
				joystickimg.transform.position = (pointVector * maxInput) + transform.position;
            }
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            currentPosition = Vector3.zero;
            //initialPress = Vector3.zero;
            //current.color = Color.clear;
			joystickimg.transform.position = imgStartPosition;
        }
    }
    
    Vector2 CalculateInput(Vector2 rawVector)
    {
        float tempMagnitude = pointVector.magnitude;
        rawVector.Normalize();
        Vector2 temp = new Vector2(Mathf.Round(rawVector.x), Mathf.Round(rawVector.y));

        float percent = Mathf.Clamp01(tempMagnitude / maxInput);
        temp *= percent;

        //temp2.text = "temp: " + temp.ToString();
        //raw2.text = "raw: " + rawVector.ToString();
        //point2.text = "percent: " + percent.ToString();

        return temp;
    }
}
