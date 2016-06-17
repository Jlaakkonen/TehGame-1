using UnityEngine;
using System.Collections;

public class ScreenOrientationController : MonoBehaviour {

    public ScreenOrientation orientation;

	void Start ()
    {
        Screen.orientation = orientation;
	}
}
