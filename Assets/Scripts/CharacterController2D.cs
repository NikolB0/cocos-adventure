using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
	[SerializeField] private float  JumpForce = 400f;							
	[Range(0, .3f)] [SerializeField] private float MovementSmoothing = .05f;	
	[SerializeField] private LayerMask WhatIsGround;							
	[SerializeField] private Transform GroundCheck;							
	[SerializeField] private Transform CeilingCheck;

	const float GroundedRadius = .2f; 
	private bool isGrounded;         
	private Rigidbody2D rb;
	private bool FacingRight = true; 
	private Vector3 Velocity = Vector3.zero;

	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();

		if (OnLandEvent == null)
		{
			OnLandEvent = new UnityEvent();
		}
	}

	private void FixedUpdate()
	{
		bool wasGrounded = isGrounded;
		isGrounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(GroundCheck.position, GroundedRadius, WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				isGrounded = true;
				if (!wasGrounded)
				{
					OnLandEvent.Invoke();
				}
			}
		}
	}


	public void Move(float move)
{
    Vector3 targetVelocity = new Vector2(move * 10f, rb.velocity.y);
    // And then smoothing it out and applying it to the character
    rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref Velocity, MovementSmoothing);

    if (move > 0 && !FacingRight)
    {
        Flip();
    }
    else if (move < 0 && FacingRight)
    {

        Flip();
    }
}

public void Jump(bool jump)
{
    if (isGrounded)
    {
        // Add a vertical force to the player.
        isGrounded = false;
        rb.AddForce(new Vector2(0f, JumpForce));
    }
}



	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		FacingRight = !FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}