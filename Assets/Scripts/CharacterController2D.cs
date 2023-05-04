using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{    
	[Range(0, .3f)]
    private float MovementSmoothing = .05f;
    
    private CircleCollider2D CircleCollider;
    private Transform CeilingCheck;

	[Header("Events")]
	[Space]
	public UnityEvent OnLandEvent;

    private Rigidbody2D rb;
    private bool FacingRight = true;
    private Vector3 Velocity = Vector3.zero;

    public void Start()
    {
        CircleCollider = GetComponent<CircleCollider2D>();
    }

    private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();

		if (OnLandEvent == null)
		{
			OnLandEvent = new UnityEvent();
		}
	}

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(CircleCollider.bounds.center, CircleCollider.bounds.extents.x, LayerMask.GetMask("Ground"));
    }


    public void Move(float move)
    {
        Vector3 targetVelocity = new Vector2(move * 10f, rb.velocity.y);

        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref Velocity, MovementSmoothing);

        if (move > 0 && !FacingRight || move < 0 && FacingRight)
        {
            FacingRight = !FacingRight;
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y);
        }
    }

    public void Jump()
    {
        float JumpForce = 700f;
        rb.AddForce(new Vector2(0f, JumpForce));
    }
}