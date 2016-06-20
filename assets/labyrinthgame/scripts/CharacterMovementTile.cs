using System.Collections;
using UnityEngine;

class CharacterMovementTile : MonoBehaviour
{
    Vector3 pos;                                // For movement
    float speed = 3.0f;                         // Speed of movement
    bool allowMovement = false;
    RaycastHit2D hitLeft;
    RaycastHit2D hitRight;
    RaycastHit2D hitUp;
    RaycastHit2D hitDown;
    public int layerMask = 1 << 14;
    

    void Start()
    {
        pos = transform.position;          // Take the initial position

    }

    void Update()
    {
        Debug.DrawRay(pos, Vector3.left, Color.green);
        Debug.DrawRay(pos, Vector3.right, Color.green);
        Debug.DrawRay(pos, Vector3.up, Color.green);
        Debug.DrawRay(pos, Vector3.down, Color.green);
       
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        if (Input.GetKey(KeyCode.A) && transform.position == pos)
        {        // Left
            hitLeft = Physics2D.Raycast(transform.position, Vector2.left, 1f, layerMask);
            if (hitLeft.collider != null)
            {
                print("Hit: " + hitLeft.collider.gameObject.name);
            }

            else
            {
                pos += Vector3.left;
            }
        }


        if (Input.GetKey(KeyCode.D) && transform.position == pos)
        {        // Right
            hitRight = Physics2D.Raycast(transform.position, Vector2.right, 1f, layerMask);
            if (hitRight.collider != null)
            {

                print("Hit: " + hitRight.collider.gameObject.name);

            }
            else
            {
                pos += Vector3.right;
            }

        }
        if (Input.GetKey(KeyCode.W) && transform.position == pos)
        {        // Up
            hitUp = Physics2D.Raycast(transform.position, Vector2.up, 1f, layerMask);
            if (hitUp.collider != null)
            {

                print("Hit: " + hitUp.collider.gameObject.name);

            }
            else
            {
                pos += Vector3.up;
            }
        }
        if (Input.GetKey(KeyCode.S) && transform.position == pos)
        {        // Down
            hitDown = Physics2D.Raycast(transform.position, Vector2.down, 1f, layerMask);
            if (hitDown.collider != null)
            {

                print("Hit: " + hitDown.collider.gameObject.name);

            }
            else
            {
                pos += Vector3.down;
            }
        }



        transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed);    // Move there
    }

    public void RightArrow()
    {
        hitRight = Physics2D.Raycast(transform.position, Vector2.right, 1f, layerMask);
        if (hitRight.collider != null)
        {

            print("Hit: " + hitRight.collider.gameObject.name);

        }
        else
        {
            pos += Vector3.right;
            transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed);
        }
       
    }

    void OnMouseClick()
    {

    }

}