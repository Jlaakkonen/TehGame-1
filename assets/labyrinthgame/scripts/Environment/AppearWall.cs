using UnityEngine;
using System.Collections;

public class AppearWall : MonoBehaviour
{

    public float DelayTime;
    public float AppearTime;
    public float DeAppearTime;
    public Sprite AppearSprite;
    public Sprite DeAppearSprite;
    float Timer = 0;
    float DelayTimer = 0;
    SpriteRenderer rend;
    Collider2D coll;

    //Add visual difference (compared to normal icewalls)
    void Update()
    {
        DelayTimer += Time.deltaTime;
        if (DelayTimer > DelayTime)
        {
            Timer += Time.deltaTime;
            if (Timer >= AppearTime)
            {
                Appear();
                if (Timer >= DeAppearTime)
                {
                    Timer = 0;
                    deAppear();
                }
            }
        }
    }

    public void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        coll = GetComponent<Collider2D>();
    }

    void Appear()
    {
        rend.sprite = AppearSprite;
        coll.enabled = true;
    }
    void deAppear()
    {
        rend.sprite = DeAppearSprite;
        coll.enabled = false;
    }
}
