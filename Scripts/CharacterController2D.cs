using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
	public static CharacterController2D instance;


	[SerializeField] public float m_JumpForce;                          // Amount of force added when the player jumps. Was 400
	[Range(0, 1)] [SerializeField] private float m_CrouchSpeed = 1f; //was .35f         // Amount of maxSpeed applied to crouching movement. 1 = 100%
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
	[SerializeField] private bool m_AirControl = false;                         // Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
	[SerializeField] private Transform m_CeilingCheck;                          // A position marking where to check for ceilings
	[SerializeField] private Collider2D m_CrouchDisableCollider;                // A collider that will be disabled when crouching

	[Header("Checkers")]
	const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	private bool isGrounded;            // Whether or not the player is grounded.
	const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 m_Velocity = Vector3.zero;
	private bool canDoubleJump;

	[Header("Hit Information")]
	public float knockBackDuration, knockBackForce;
	private float knockBackCounter;

	[Header("Movement, Layers, and Animators")]
	float horizontalMove = 0f;
	public float runSpeed;
	public float jumpHangTime = .2f;
	public Animator animator;
	public LayerMask interactableLayer;
	public GameObject target;
	public Vector3 interactSpaceOffset;
	private Vector3 dialogOffset;


	bool jump = false;
	bool crouch = false;
	float hangCounter;
	public bool Paused{get; set;}

	public bool DoubleJump { get { return canDoubleJump; } }
	public bool Grounded { get { return isGrounded; } }
	public Vector3 DialogOffset {get{ return dialogOffset; } }

	[SerializeField]
	private Collider2D collider;

	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	public BoolEvent OnCrouchEvent;
	private bool m_wasCrouching = false;



	private void Awake()
	{
		if (instance == null)
			instance = this;
		else
			Destroy(gameObject);

		DontDestroyOnLoad(gameObject);

		Paused = false;

		m_Rigidbody2D = GetComponent<Rigidbody2D>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();

		if (OnCrouchEvent == null)
			OnCrouchEvent = new BoolEvent();
	}
    /*private void Start()
    {
		if (instance == null)
			instance = this;
		else
			Destroy(gameObject);

		DontDestroyOnLoad(gameObject);
    }*/

    private void Update()
    {
		//Coyote/Hang Time forgiveness
		if (isGrounded)
			hangCounter = jumpHangTime;
		else
			hangCounter -= Time.deltaTime;
	}

    private void FixedUpdate()
	{
		//GroundCheck();

		if (Paused)
		{
			animator.SetFloat("MoveX", 0f);
			Move(0f, false, false);
		}
		else
		{
			Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
			jump = false;
		}
	}
	void GroundCheck()
	{
		isGrounded = false;
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		if (colliders.Length > 0)
			isGrounded = true;
	}


	public void Move(float move, bool crouch, bool jump)
	{
		if (knockBackCounter <= 0)
		{
			if (!crouch)
			{
				// If the character has a ceiling preventing them from standing up, keep them crouching
				if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
				{
					crouch = true;
				}
			}

			//only control the player if grounded or airControl is turned on
			if (isGrounded || m_AirControl)
			{
				// If crouching
				/*if (crouch)
				{
					if (!m_wasCrouching)
					{
						m_wasCrouching = true;
						OnCrouchEvent.Invoke(true);
					}

					// Reduce the speed by the crouchSpeed multiplier
					move *= m_CrouchSpeed;

					// Disable one of the colliders when crouching
					if (m_CrouchDisableCollider != null)
						m_CrouchDisableCollider.enabled = false;
				}
				else
				{
					// Enable the collider when not crouching
					if (m_CrouchDisableCollider != null)
						m_CrouchDisableCollider.enabled = true;

					if (m_wasCrouching)
					{
						m_wasCrouching = false;
						OnCrouchEvent.Invoke(false);
					}
				}*/

				// Move the character by finding the target velocity
				Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
				// And then smoothing it out and applying it to the character
				m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

				// If the input is moving the player right and the player is facing left...
				if (move > 0 && !m_FacingRight)
				{
					// ... flip the player.
					Flip();
				}
				// Otherwise if the input is moving the player left and the player is facing right...
				else if (move < 0 && m_FacingRight)
				{
					// ... flip the player.
					Flip();
				}
			}

				
			// If the player should jump...
			if (jump && hangCounter > 0f) //was if(m_Grounded && jump)	hangCounter > 0f
			{
				// Add a vertical force to the player.
				isGrounded = false;
				//m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));		OLD JUMP LOGIC
				m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, m_JumpForce);
			}
			else if (!isGrounded && jump)
			{
				if (canDoubleJump)
				{
					canDoubleJump = false;
					//m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce - 120));	OLD JUMP LOGIC
					m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, m_JumpForce);
				}
			}
		}
		else
		{
			knockBackCounter -= Time.deltaTime;
			if (m_FacingRight)
			{
				m_Rigidbody2D.velocity = new Vector2(-knockBackForce, m_Rigidbody2D.velocity.y);
			}
			else
			{
				m_Rigidbody2D.velocity = new Vector2(knockBackForce, m_Rigidbody2D.velocity.y);
			}
		}
		// If crouching, check to see if the character can stand up	
	}

	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;
		Shooting.instance.FacingRight = !Shooting.instance.FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;

	}

	public void KnockBack()
	{
		knockBackCounter = knockBackDuration;
		m_Rigidbody2D.velocity = new Vector2(0f, knockBackForce);
	}

    public void HandleUpdate()
    {
		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
		animator.SetFloat("MoveX", Mathf.Abs(horizontalMove));
		animator.SetBool("IsGrounded", isGrounded);

		isGrounded = Physics2D.OverlapCircle(m_GroundCheck.position, .2f, m_WhatIsGround);

		if (isGrounded)
		{
			canDoubleJump = true;
		}

		if (Paused)
		{
			animator.SetFloat("MoveX", 0f);
			Move(0f, false, false);
		}





		if (Input.GetButtonDown("Jump"))
		{
			if (isGrounded)
			{
				jump = true;
				//animator.SetBool("IsJumping", true);
			}
			else
			{
				if (canDoubleJump) 
				{
					jump = true;
					animator.SetBool("IsGrounded", false);
					//animator.SetBool("IsDoubleJumping", true);
				}
				
			}

		}
		if (Input.GetButtonDown("Crouch"))
		{
			crouch = true;
			animator.SetBool("IsCrouching", true);
			//ChangeOrderInLayer(1);
		}
		if (Input.GetKeyDown(KeyCode.F))
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
			collider.GetComponent<Interactable>()?.Interact();  //Use the null operator "?" in case null is returned. Avoids the error.
		}

	}
}


