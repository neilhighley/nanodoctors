using UnityEngine;
using System.Collections;
using System;

public class PlayerScript : MonoBehaviour {
    public float speed = 10f;
    public float turnSpeed = 60f;
    public float turnSmoothing = 10f;

    public float JumpSpeed = 30.0f;


    private Vector3 movement;
    private Vector3 turning;
    private Rigidbody playerRB;
    private Animation playerAnim;

    private bool IsJumping = false; 

    void Awake()
    {
        Debug.Log("Player Awake");
        playerAnim = GetComponentInChildren<Animation>();
        playerRB = GetComponent<Rigidbody>();

    }

    void FixedUpdate()
    {
        CharacterController controller = GetComponent<CharacterController>();
       
        if (IsJumping && playerRB.position.y==0)
        {
            IsJumping = false;
            playerAnim.Play("Idle");
        }
        float lh = Input.GetAxisRaw("Horizontal");
        float lv = Input.GetAxisRaw("Vertical");

        bool jumpPressed = Input.GetButtonDown("Jump");
        if (jumpPressed) DoJump();

        Move(lh, lv, jumpPressed);
        Animating(lh, lv);
       
    }

    private void DoJump()
    {
        playerAnim.Play("JumpA");
        IsJumping = true;
    }

    void Move(float lh, float lv,bool jumpPressed)
    {
        movement.Set(lh, 0f, lv);
        if (jumpPressed)
        {
            Debug.Log("Jump");
            movement.y = JumpSpeed;
            playerAnim.Play("JumpA");
            IsJumping = false;
        }
        movement = movement.normalized * speed * Time.deltaTime;
        playerRB.MovePosition(transform.position + movement);

        if(lh!=0f || lv != 0f)
        {
            Rotating(lh, lv);
        }

    }

    void Animating(float lh, float lv)
    {
        bool isRunning = lh != 0f || lv != 0f;
        playerAnim.Play(isRunning?"Run":"Idle");
    }

    void Rotating(float lh,float lv)
    {
        Vector3 targetDirection = new Vector3(lh, 0f, lv);
        Quaternion targetRot = Quaternion.LookRotation(targetDirection, Vector3.up);
        Quaternion newRot = Quaternion.Lerp(playerRB.rotation, targetRot, turnSmoothing * Time.deltaTime);
        playerRB.MoveRotation(newRot);
    }
}