using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MeltableWall : MonoBehaviour {

    bool InRange;
    Collector playerscript;//Fetch from LabManager instead of GetComponent
    public Sprite MeltSprite;
    Animator anim;

    void Start ()
    {
        anim = GetComponent<Animator>();
    }
    void OnMouseDown()
    {
        if (InRange)
        {
         //   if (playerscript.GetTorches()>0)
         //   {
         //       playerscript.DevourTorches();
                BoxCollider2D col = gameObject.GetComponent<BoxCollider2D>();
                col.enabled = false;
                anim.Play("MeltAnim");
                Destroy(this);
         //   }
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            playerscript = col.gameObject.GetComponent<Collector>();
            InRange = true;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            playerscript = null;
            InRange = false;
        }
    }
}
