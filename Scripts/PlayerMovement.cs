using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;

    public CharacterController2D controller;
    public Animator animator;
    public LayerMask interactableLayer;
    public GameObject target;
    public Vector3 interactSpaceOffset;

    public event Action OnEncountered;


    float horizontalMove = 0f;
    public float runSpeed;
    bool jump = false;
    bool crouch = false;
    private Vector3 dialogOffset;

    public bool Paused { get; set; }
    public Vector3 DialogOffset
    { get { return dialogOffset; } }


    public void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        animator.SetFloat("MoveX", Mathf.Abs(horizontalMove));


        if (controller.Grounded)
        {
            //animator.SetBool("IsJumping", false);
            animator.SetBool("IsDoubleJumping", false);
        }


        if (Input.GetButtonDown("Jump"))
        {
            if (controller.Grounded)
            {
                jump = true;
                animator.SetBool("IsJumping", true);
            }
            else
            {
                jump = true;
                animator.SetBool("IsJumping", false);
                //animator.SetBool("IsDoubleJumping", true);
            }

        }
        if (Input.GetButtonDown("Crouch")) 
        {
            crouch = true;
            animator.SetBool("IsCrouching", true);
            //ChangeOrderInLayer(1);
        }
        if (Input.GetButtonDown("Submit"))
        {
            if (Physics2D.OverlapCircle(target.transform.position + interactSpaceOffset, .2f, interactableLayer) != null)
            {
                Interact();
            }
        }

    }

    private void Interact()
    {
        var interactPos = transform.position + interactSpaceOffset;
        var collider = Physics2D.OverlapCircle(interactPos, .3f, interactableLayer);
        if (collider != null)
        {
            Paused = true;
            dialogOffset = collider.transform.position;
            //collider.GetComponent<Interactable>()?.Interact();  //Use the null operator "?" in case null is returned. Avoids the error.
        }

    }

    private void Awake()
    {
        instance = this;
        
        Paused = false;   
    }

    public void HitAnim()
    {
        animator.SetTrigger("IsHit");
    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }

    private void FixedUpdate()
    {

        if (Paused)
        {
            animator.SetFloat("MoveX", 0f);
            controller.Move(0f, false, false);
        }
        else
        {
            controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
            jump = false;
        }

        

    }


    /*public void ChangeOrderInLayer(int orderInLayer)
    {
        target.GetComponent<SpriteRenderer>().sortingOrder = orderInLayer;
    }*/

}