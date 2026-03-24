/*****************************************************************************
// File Name : Comments.cs
// Author : John P. Doran
// Creation Date : February 19, 2020
//
// Brief Description : This is a sample document that teaches students how to
comment. Students have to follow this commenting style
exactly so that they don't get points deducted.
*****************************************************************************/
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    //[SerializeField] private Transform orientation;
    private InputAction move;
    private Vector3 playerMovement;
    //private Vector3 moveDirection;
    private bool isMoving;
    [SerializeField] private float playerSpeed;
    [SerializeField] private float slowSpeed;
    private Rigidbody rb;


    private void Awake()
    {
        move = InputSystem.actions.FindAction("Move");

        move.performed += MovePerformed;
        move.canceled += MoveCanceled;
    }


    void Start()
    {
        rb = GetComponent<Rigidbody>();

        

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void MovePerformed(InputAction.CallbackContext obj)
    {
        playerMovement.x = obj.ReadValue<Vector2>().x;
        playerMovement.z = obj.ReadValue<Vector2>().y;
        isMoving = true;
    }

    private void MoveCanceled(InputAction.CallbackContext obj)
    {
        playerMovement = Vector3.zero;
        isMoving = false;
    }

    void Update()
    {
        //new movement stuff
        //moveDirection = orientation.forward * playerMovement.z + orientation.right * playerMovement.x;
        //rb.AddForce(moveDirection.normalized * playerSpeed, ForceMode.Force);

        //Old movement stuff
        rb.AddRelativeForce(new Vector3(playerMovement.x * playerSpeed, 0.0f, playerMovement.z * playerSpeed));

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

    private void OnDestroy()
    {
        move.performed -= MovePerformed;
        move.canceled -= MoveCanceled;
    }
}
