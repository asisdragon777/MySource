using UnityEngine;
using System.Collections;

public class RobotControllerScript : MonoBehaviour {

	public float maxSpeed = 10f;
	bool facingRight = true;
	Animator anim;

	bool grounded = false;
	public Transform groundCheck;
	float groundRadius = 0.2f;
	public LayerMask whatIsGround;
	public float jumpForce = 700f;
	public float CrouchForce = 50f;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}

	// Update is called once per frame
	void FixedUpdate () {
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		anim.SetBool ("Ground", grounded);
		anim.SetFloat ("vSpeed", rigidbody2D.velocity.y);

//		if (Input.GetKeyUp (KeyCode.DownArrow)) {
//						anim.SetFloat ("CrouchSpeed", rigidbody2D.velocity.y);
//				}
//		anim.SetFloat ("CrouchSpeed", rigidbody2D.velocity.y);



		float move = Input.GetAxis ("Horizontal");
		float move_v = Input.GetAxis ("Vertical");

		anim.SetFloat ("Speed", Mathf.Abs (move));
		//rigidbody2D.velocity = new Vector2 (move * maxSpeed, rigidbody2D.velocity.y);
		rigidbody2D.velocity = new Vector2 (move * maxSpeed, rigidbody2D.velocity.y);

		if (move > 0 && !facingRight) {
			Flip ();	
		} else if (move < 0 && facingRight) {
			Flip();
		}
	}


	void Update () {
		if (grounded && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))) {
			anim.SetBool ("Ground", false);
			rigidbody2D.AddForce(new Vector2(0, jumpForce));
		}

		if (grounded && (Input.GetKeyDown (KeyCode.DownArrow))) {
			anim.SetBool ("Ground", false);
			anim.SetFloat ("CrouchSpeed", Input.GetAxis ("Vertical") + CrouchForce);
			rigidbody2D.AddForce(new Vector2(0, Input.GetAxis ("Vertical") + CrouchForce));
		}

//		if (grounded) {
//			//anim.SetBool ("Ground", false);
//
//
//
//
//			if (Input.GetKeyUp (KeyCode.DownArrow)) {
//				anim.SetFloat ("CrouchSpeed", rigidbody2D.velocity.y);
//			}
//
//
//		}


	}

	void Flip()	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;

	}
}
