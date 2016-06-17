using UnityEngine;
using System.Collections;

public class SpinBlades : MonoBehaviour {

    Rigidbody2D rgb;
    public float rotationSpeed;

	void Start ()
    {
        rgb = gameObject.GetComponent<Rigidbody2D>();
	}
	void Update ()
    {
        Rotatedis();
	}
    void Rotatedis ()
    {
        if (Mathf.Abs(rgb.rotation) > 360)
        {
            rgb.rotation = rgb.rotation % 360;
        }
        else
        {
            rgb.rotation += rotationSpeed * Time.deltaTime;
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
			LabManager.manager.RestartLevel();
        }
    }
}
