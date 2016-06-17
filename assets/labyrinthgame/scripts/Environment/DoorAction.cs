using UnityEngine;
using System.Collections;

public class DoorAction : SwitchTarget
{
    Animator anim;
    bool doorOpen = false;
    public bool doorClosedOnStart = false;

	void Start ()
    {
        anim = GetComponent<Animator>();
        doorOpen = doorClosedOnStart;
        Action();
	}

    //Custom action for opening tala-door
    public override void Action ()
    {
        switch (doorOpen)
        {
            case false:
                doorOpen = true;
                anim.SetBool("doorOpen", true);
                break;
            case true:
                doorOpen = false;
                anim.SetBool("doorOpen", false);
                break;
        }
    }
}
