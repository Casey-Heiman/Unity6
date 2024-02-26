using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public enum PlayerNumber { Player1, Player2 }
    public PlayerNumber playerNumber;
    public float speed = 3;
    public float rotationSpeed = 90;
    public float gravity = -20f;
    public float jumpSpeed = 15;

    private KeyCode moveForwardKey;
    private KeyCode moveBackwardKey;
    private KeyCode rotateLeftKey;
    private KeyCode rotateRightKey;
    private KeyCode jumpKey;

    CharacterController characterController;
    Vector3 moveVelocity;
    Vector3 turnVelocity;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();

        
        if (playerNumber == PlayerNumber.Player1)
        {
            moveForwardKey = KeyCode.W;
            rotateLeftKey = KeyCode.A;
            rotateRightKey = KeyCode.D;
            jumpKey = KeyCode.S;
        }


        else if (playerNumber == PlayerNumber.Player2)
        {
            moveForwardKey = KeyCode.I;
            moveBackwardKey = KeyCode.K;
            rotateLeftKey = KeyCode.J;
            rotateRightKey = KeyCode.L;
            jumpKey = KeyCode.K; // Player 2's jump key
        }
    }

    void Update()
    {
        var hInput = 0f;
        var vInput = 0f;

        // Player 1 movement
        if (playerNumber == PlayerNumber.Player1)
        {
            hInput = Input.GetAxis("Horizontal");
            vInput = Input.GetAxis("Vertical");
        }
        // Player 2 movement
        else if (playerNumber == PlayerNumber.Player2)
        {
            hInput = Input.GetAxis("Horizontal2");
            vInput = Input.GetAxis("Vertical2");
        }

        if (characterController.isGrounded)
        {
            moveVelocity = transform.forward * speed * vInput;
            turnVelocity = transform.up * rotationSpeed * hInput;
            if (Input.GetKeyDown(jumpKey))
            {
                moveVelocity.y = jumpSpeed;
            }
        }
        //Adding gravity
        moveVelocity.y += gravity * Time.deltaTime;
        characterController.Move(moveVelocity * Time.deltaTime);
        transform.Rotate(turnVelocity * Time.deltaTime);
    }
}
