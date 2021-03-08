using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{

    [SerializeField] Transform player;
    [SerializeField] float aggroRange;
    [SerializeField] float walkSpeed;
    [SerializeField] float runSpeed;
    [SerializeField] Transform groundDetection;
    [SerializeField] float distanceFromObstacleToStop;
    [SerializeField] Transform castPoint;

    Rigidbody2D theRb;
    Animator theAnim;

    bool facingRight = true;
    bool isSearching = false;
    int pickADirection;
    float deAggroTime = 3;
    float moveLength;
    public bool isAggravated { get; set; }

    
    

    private void Start()
    {
        moveLength = 2;
        RandomDirectionalChoice();
        isAggravated = false;
        theRb = GetComponent<Rigidbody2D>();
        theAnim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (IsFacingRight())
            theRb.velocity = new Vector2(runSpeed, 1f);
        else
            theRb.velocity = new Vector2(-runSpeed, 1f);

        if (CanSeePlayer(aggroRange))
        {
            isAggravated = true;
        }
        else 
        {
            if (isAggravated)
            {
                if (!isSearching)
                {
                    isSearching = true;
                }
                else
                    deAggroTime -= Time.deltaTime;
            }
            else
                StrafingLeftAndRight();
                
           
        }
        

        if (isAggravated) 
        {
            deAggroTime = 3;
            ChasePlayer();
        }
        if (deAggroTime <= 0)
            StopChasingPlayer();

    }

    bool CanSeePlayer(float distance) 
    {
        bool val = false;
        float castDist = distance;

        if (!facingRight)
            castDist = -distance;



        Vector2 endPos = castPoint.position + Vector3.right * castDist;


        RaycastHit2D theAggroRange = Physics2D.Linecast(castPoint.position, endPos, 1 << LayerMask.NameToLayer("Player"));

        if (theAggroRange.collider != null)
        {
            if (theAggroRange.collider.gameObject.CompareTag("Player"))
                val = true;
            else
                val = false;

            Debug.DrawLine(castPoint.position, theAggroRange.point, Color.yellow);  
        }
        else
            Debug.DrawLine(castPoint.position, theAggroRange.point, Color.blue);

        return val;
      
    }

    

    void ChasePlayer() 
    {
        theAnim.SetBool("IsMoving", true);
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, transform.right, distanceFromObstacleToStop); //was Vector2.right

        if (transform.position.x < player.position.x)
        {
            //Enemy is to the leftside of player, move right
            //theRb.velocity = new Vector2(moveSpeed, 0);
            //transform.localScale = new Vector2(1, transform.localScale.y);   //Better way to flip than sprite renderer
            //facingRight = true;
            DirectionalInput(1);

        }
        else 
        {
            //Enemy is to the rightside of player, move left
            //theRb.velocity = new Vector2(-moveSpeed, 0);
            //transform.localScale = new Vector2(-1, transform.localScale.y);
            //facingRight = false;
            DirectionalInput(2);
        }
    }
    
    void StopChasingPlayer() 
    {
        isAggravated = false;
        isSearching = false;
        theAnim.SetBool("IsMoving", false);
        theRb.velocity = new Vector2(0, 0);
        deAggroTime = 3;
        //Could also = new Vector2(0, 0);
    }

    void RandomMovements() 
    { 
        if (moveLength > 0)
        {
            moveLength -= Time.deltaTime;
            theAnim.SetBool("IsMoving", true);


                //theRb.velocity = new Vector2(-(moveSpeed / 2), 0f);

            //DirectionalInput(pickADirection);


            //theAnim.SetBool("IsMoving", true);

             transform.Translate(Vector2.right * (walkSpeed) * Time.deltaTime);
             RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, 1f, 0 << LayerMask.NameToLayer("Ground"));
             if (groundInfo.collider)
             {
                 transform.Translate(Vector2.right * (walkSpeed) * Time.deltaTime);
                 if (facingRight)
                 {
                     transform.eulerAngles = new Vector3(0, -180, 0);
                     facingRight = false;
                 }
                 else
                 {
                     transform.eulerAngles = new Vector3(0, 0, 0);
                     facingRight = true;
                 }
             }
        }
        else 
        {
            theAnim.SetBool("IsMoving", false);
            StartCoroutine(MomentaryPause());
        
        }
    }

    void StrafingLeftAndRight() 
    {
        bool isResting = false;
        if (moveLength > 0)
        {
            isResting = true;
            theAnim.SetBool("IsMoving", true);
            if (IsFacingRight())
                theRb.velocity = new Vector2(walkSpeed, 0f);
            else
                theRb.velocity = new Vector2(-walkSpeed, 0f);

            moveLength -= Time.deltaTime;
        }
        if(moveLength <= 0)
        {
            theRb.velocity = new Vector2(0, 0);

            if (isResting) 
            {
                //theRb.velocity = new Vector2(0, 0);
                isResting = false;
                StartCoroutine(MomentaryPause());
            } 
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isAggravated) 
        {
            StopChasingPlayer();
            deAggroTime = 0;
        }
        transform.localScale = new Vector2(-(Mathf.Sign(theRb.velocity.x)), transform.localScale.y);
    }

    void RandomDirectionalChoice() 
    {
        pickADirection = Random.Range(1, 3);
    }

    void DirectionalInput(int choice) 
    {
        if (choice == 1)
        {
            //Enemy is to the leftside of player, move right
            theRb.velocity = new Vector2(runSpeed, 0);
            transform.localScale = new Vector2(1, transform.localScale.y);   //Better way to flip than sprite renderer
            facingRight = true;

        }
        else 
        {
            //Enemy is to the rightside of player, move left
            theRb.velocity = new Vector2(-runSpeed, 0);
            transform.localScale = new Vector2(-1, transform.localScale.y);
            facingRight = false;

        }
    }
    bool IsFacingRight() 
    {
        return transform.localScale.x > Mathf.Epsilon;
    }
    public IEnumerator MomentaryPause() 
    {
        Debug.Log("Coroutine Started");
        theAnim.SetBool("IsMoving", false);
        theRb.velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(3f);
        moveLength = Random.Range(2f, 3f);
        //RandomDirectionalChoice();
        yield break;
    }
}
