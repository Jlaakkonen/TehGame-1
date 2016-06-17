using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MazeGuard : MonoBehaviour {

    Rigidbody2D rb2d;
    public Transform[] wayPointList;
    Transform targetWayPoint;
    public float speed = 5f;
    int currentWayPoint = 0;
    public float arrivalDistance = 0.1f;
    float lastDistanceToTarget = 0f;
	Animator anim;
	Vector3 dir;
	float dirX, dirY;

    //Rewrite
    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator> ();
        targetWayPoint = wayPointList[currentWayPoint];
        lastDistanceToTarget = Vector3.Distance(transform.position, targetWayPoint.position);
    }
    void FixedUpdate()
    {
        walko();
    }
    void walko()
    {
        float distanceToTarget = Vector3.Distance(transform.position, targetWayPoint.position);
        if ((distanceToTarget < arrivalDistance) || (distanceToTarget > lastDistanceToTarget))
        {
            currentWayPoint++;
            if (currentWayPoint >= wayPointList.Length)
                currentWayPoint = 0;
            targetWayPoint = wayPointList[currentWayPoint];
            lastDistanceToTarget = Vector3.Distance(transform.position, targetWayPoint.position);
            //Debug.Log("Added the next waypoint(" + currentWayPoint + "). Object: " + gameObject.name);
        }
        dir = (targetWayPoint.position - transform.position).normalized;
		dirX = Mathf.Round (dir.x);
		dirY = Mathf.Round (dir.y);

		//Debug.Log ("Swippers direction: " + dir);
		//Debug.Log ("Swippers ClampX: " + dirX);
		//Debug.Log ("Swippers ClampY: " + dirY);

		if (dirX == 0f && dirY == 1f)
		{
			//Debug.Log ("Ylös");
			anim.SetBool("GoUp", true);
			anim.SetBool("GoDown", false);
			anim.SetBool("GoRight", false);
			anim.SetBool("GoLeft", false);
		}
		else if (dirX == 0f && dirY == -1f)
		{
			//Debug.Log ("Alas");
			anim.SetBool("GoDown", true);
			anim.SetBool("GoUp", false);
			anim.SetBool("GoRight", false);
			anim.SetBool("GoLeft", false);
		}
		else if (dirX == 1f && dirY == 0f)
		{
			//Debug.Log ("Oikea");
			anim.SetBool("GoRight", true);
			anim.SetBool("GoUp", false);
			anim.SetBool("GoDown", false);
			anim.SetBool("GoLeft", false);
		}
		else if (dirX == -1f && dirY == 0f)
		{
			//Debug.Log ("Vasen");
			anim.SetBool("GoLeft", true);
			anim.SetBool("GoUp", false);
			anim.SetBool("GoDown", false);
			anim.SetBool("GoRight", false);
		}

        rb2d.MovePosition(transform.position + dir * (speed * Time.fixedDeltaTime));
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
			LabManager.manager.RestartLevel ();
        }
    }
}
