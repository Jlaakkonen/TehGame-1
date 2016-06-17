using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Torches : MonoBehaviour {

    bool InRange;
    public bool hasTorch;
    Collector playerscript;
    public GameObject Torch;
    GameObject TorchInstance;
    public Sprite LitTorch;
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }
    void OnMouseDown()
    {
        if (InRange)
        {
            if (playerscript.GetTorches() > 0 && hasTorch == false)
            {
                playerscript.DevourTorches();
                BoxCollider2D col = gameObject.GetComponent<BoxCollider2D>();
                TorchInstance = (GameObject)Instantiate(Torch,new Vector3(transform.position.x,transform.position.y, -1.49f),transform.rotation);
                anim.SetBool("Lit", true);
                hasTorch = true;
                //Destroy(this);
            }
            else if (hasTorch == true)
            {
                Destroy(TorchInstance);
                playerscript.GainTorches();
                hasTorch = false;
                anim.SetBool("Lit", false);
            }
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            //LabManager pls
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
