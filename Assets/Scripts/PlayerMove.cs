/*****************************************************************************
// File Name : PlayerMove.cs
// Author : Will Northrup
// Creation Date : 3/24/2026
//
// Brief Description : This is a script that is attached to the player/slime
game object. This script allows for movement using the WASD keys 
*****************************************************************************/
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Transform orientation;
    private InputAction move;
    private Vector3 playerMovement;
    private Vector3 moveDirection;
    private bool isMoving;
    [SerializeField] private float playerSpeed;
    [SerializeField] private float slowSpeed;
    private Rigidbody rb;

    public float PlayerSpeed { get => playerSpeed; set => playerSpeed = value; }

    /// <summary>
    /// Sets the inputs when the scene reloads
    /// </summary>
    private void Awake()
    {
        move = InputSystem.actions.FindAction("Move");

        move.performed += MovePerformed;
        move.canceled += MoveCanceled;
    }

    /// <summary>
    /// Locks mouse in center of screen and hides it at start
    /// </summary>
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    /// <summary>
    /// Reads the direction of input in x and z axis for player movement
    /// </summary>
    /// <param name="obj"></param>
    private void MovePerformed(InputAction.CallbackContext obj)
    {
        playerMovement.x = obj.ReadValue<Vector2>().x;
        playerMovement.z = obj.ReadValue<Vector2>().y;
        isMoving = true;
    }

    /// <summary>
    /// Sets the player movement variable to zero
    /// </summary>
    /// <param name="obj"></param>
    private void MoveCanceled(InputAction.CallbackContext obj)
    {
        playerMovement = Vector3.zero;
        isMoving = false;
    }

    /// <summary>
    /// Allows the player to move with relative force, but clamps max and min speed of player
    /// </summary>
    void Update()
    {
        //testing new movement stuff
        moveDirection = orientation.forward * playerMovement.z + orientation.right * playerMovement.x;
        rb.AddForce(moveDirection.normalized * playerSpeed, ForceMode.Force);

        //Old movement stuff that adds relative force to the player
        //rb.AddRelativeForce(new Vector3(playerMovement.x * playerSpeed, 0.0f, playerMovement.z * playerSpeed));

        //clamps the player speed
        rb.linearVelocity = new Vector3(Mathf.Clamp(rb.linearVelocity.x, -playerSpeed, playerSpeed),
            rb.linearVelocity.y, Mathf.Clamp(rb.linearVelocity.z, -playerSpeed, playerSpeed));

        //if player stops moving, the force stops being applied and stops the player movement
        if (isMoving == false)
        {
            rb.linearVelocity = new Vector3(Mathf.Clamp(rb.linearVelocity.x * slowSpeed, -playerSpeed * slowSpeed,
                playerSpeed * slowSpeed),
                rb.linearVelocity.y, Mathf.Clamp(rb.linearVelocity.z * slowSpeed, -playerSpeed * slowSpeed,
                playerSpeed * slowSpeed));
        }
    }

    /// <summary>
    /// Disconnects the move input action when destroyed
    /// </summary>
    private void OnDestroy()
    {
        move.performed -= MovePerformed;
        move.canceled -= MoveCanceled;
    }
}
