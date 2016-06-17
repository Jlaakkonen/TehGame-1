using UnityEngine;
using System.Collections;

public class Catcher : MonoBehaviour {

	float normalscaleX,normalscaleY;
	bool largesize = false;

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Items")
        {
			Destroy(col.gameObject);
        }
    }
		
	public void LargeBucket()
	{
		if (largesize == false) {
			normalscaleX = transform.localScale.x;
			normalscaleY = transform.localScale.y;
			transform.localScale = new Vector2 (normalscaleX + 0.3f, normalscaleY + 0.2f);
			largesize = true;
			Invoke ("Normalsize", 5f);
		}
	}

	void Normalsize()
	{
		transform.localScale = new Vector2 (normalscaleX, normalscaleY);
		largesize = false;
	}
}
