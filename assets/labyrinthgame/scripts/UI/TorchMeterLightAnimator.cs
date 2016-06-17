using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class TorchMeterLightAnimator : MonoBehaviour {

	public float animationSpeed;
	public Sprite[] lightImg;
	Image imgComp;
	float startSpeed;
	int c = 0;

	void Start()
	{
		imgComp = GetComponent<Image> ();
		startSpeed = animationSpeed;
		Debug.Log ("StartSPD: " + startSpeed);
	}

	void FixedUpdate()
	{
		if(animationSpeed > 0f)
		{
			animationSpeed -= Time.deltaTime;
		}
		else
		{
			if (c <= lightImg.Length) 
			{
				imgComp.sprite = lightImg [c];
				animationSpeed = startSpeed;
				c++;
				if(c == lightImg.Length)
				{
					c = 0;
				}
			}
		}
	}
}
