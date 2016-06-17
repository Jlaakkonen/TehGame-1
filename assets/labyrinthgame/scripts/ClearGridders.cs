using UnityEngine;
using System.Collections;
//[ExecuteInEditMode]
public class ClearGridders : MonoBehaviour {

	void Start ()
    {
        Gridder[] gridders = FindObjectsOfType<Gridder>();
        foreach (Gridder item in gridders)
        {
            Destroy(item);
        }
        Debug.Log("Desutroy~");
        Destroy(gameObject);
	}
}
