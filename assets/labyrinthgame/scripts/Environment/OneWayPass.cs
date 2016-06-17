using UnityEngine;
using System.Collections;

public class OneWayPass : MonoBehaviour
{
    Collider2D coll;

	void Start ()
    {
        coll = GetComponent<Collider2D>();
	}

    public void DisableCollider()
    {
        coll.enabled = false;
    }

    public void EnableCollider()
    {
        coll.enabled = true;
    }
}
