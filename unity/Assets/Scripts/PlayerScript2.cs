using UnityEngine;
using System.Collections;

public class PlayerScript2 : MonoBehaviour {
    private Animation playerAnim;
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;
    private Vector3 movement;
    private Vector3 turning;
    private Rigidbody playerRB;
    private bool IsJumping = false;
    void Awake()
    {
        playerAnim = GetComponentInChildren<Animation>();
        playerRB = GetComponent<Rigidbody>();

    }
    // Use this for initialization
    void Start () {
	
	}


    void FixedUpdate()
    {
        CharacterController controller = GetComponent<CharacterController>();
        if (controller.isGrounded)
        {
            if (IsJumping)
            {
                playerAnim.Play("Idle");
                IsJumping = false;
            }
            var lh = Input.GetAxis("Horizontal");
            var lv = Input.GetAxis("Vertical");

            moveDirection = new Vector3(lh, 0, lv);
            moveDirection=moveDirection.normalized* speed*Time.deltaTime;
            //moveDirection = transform.TransformDirection(moveDirection);

            moveDirection *= speed;
            if (lh != 0 || lv != 0)
                playerAnim.Play("Run");

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
                playerAnim.Play("JumpA");
                IsJumping = true;
            }
        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }
}
