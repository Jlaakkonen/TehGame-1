using UnityEngine;
using System.Collections;

public class KeyGate : MonoBehaviour
{
	Animator anim;

	void Start()
	{
		anim = GetComponent<Animator> ();
	}

    public void OpenDoor (bool hasKey)
    {
        if(hasKey)
		{
			anim.SetBool ("Unlock", true);
			Invoke("DestroyDoor", anim.GetNextAnimatorStateInfo(0).length +0.5f);
        }
        else
        {
            //no key, play sound effect
        }
    }

	void DestroyDoor()
	{
		Destroy (gameObject);
	}
}
