using UnityEngine;
using System.Collections;

public class BreakingFloor : MonoBehaviour
{
    bool floor = true;
    Animator anim;
    Collider2D col;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("Hello");
            StartCoroutine(BreakFloor());
             
            //animation.Play("Collapse");
            
            if (floor == false)
            {
                LabManager.manager.RestartLevel();
            }
        }

    }

    public IEnumerator BreakFloor()
    {
        anim.SetBool("Break", true);
        yield return new WaitForSeconds(1.65f);
        Debug.Log("pleb");
        floor = false;
        //anim.SetBool("Break", true);
        //Invoke("BreakFloor", anim.GetNextAnimatorStateInfo(0).length + 0.5f);
    }


}
