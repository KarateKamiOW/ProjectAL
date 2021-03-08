using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCPatrol : MonoBehaviour
{
    public float speed;
    public float distance;
    public Transform groundDetection;
    public Animator anim;

    bool movingRight = true;

    private void Update()
    {

        transform.Translate(Vector2.right * speed * Time.deltaTime);
        anim.SetBool("IsMoving", true);

        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);
        if (groundInfo.collider == false) 
        {
            if (movingRight)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else 
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
    }
}
