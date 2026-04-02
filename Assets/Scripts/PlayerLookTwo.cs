/*****************************************************************************
// File Name : PlayerLookTwo.cs
// Author : Will Northrup
// Creation Date : 3/24/2026
//
// Brief Description : This is an experimental script that offers a potential
second way to manage the player camera.
*****************************************************************************/
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLookTwo : MonoBehaviour
{
    private InputAction move;
    private Vector3 playerMovement;
    [SerializeField] private Transform orientation;
    [SerializeField] private Transform playerObj;
    [SerializeField] private Rigidbody rb;

    [SerializeField] private float rotationSpeed;

    /// <summary>
    /// makes the mouse dissapear and gets the move input action
    /// </summary>
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        move = InputSystem.actions.FindAction("Move");

        move.performed += MovePerformed;
        move.canceled += MoveCanceled;
    }
    
    /// <summary>
    /// Sets the playerMovement Vector3 to zero when input is canceled
    /// </summary>
    /// <param name="obj"></param>
    private void MoveCanceled(InputAction.CallbackContext obj)
    {
        playerMovement = Vector3.zero;
    }

    /// <summary>
    /// reads the direction of the input for when the input is performed
    /// </summary>
    /// <param name="obj"></param>
    private void MovePerformed(InputAction.CallbackContext obj)
    {
        playerMovement.x = obj.ReadValue<Vector2>().x;
        playerMovement.z = obj.ReadValue<Vector2>().y;
    }

    /// <summary>
    /// This allows the player to move relative to the direction of their camera on the x and z axis
    /// </summary>
    void Update()
    {
        //updates the viewDir variable and sets the orientation to face that direction.
        Vector3 viewDir = playerObj.position - new Vector3(transform.position.x,
            playerObj.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

        //updates the inputDir variable to have the player movement be relative to the orientation
        Vector3 inputDir = orientation.forward * playerMovement.z + orientation.right * playerMovement.x;

        //if the inputDir variable is not equal to zero, the player object will rotate to face in that direction
        if (inputDir != Vector3.zero)
        {
            playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
        }
    }

    /// <summary>
    /// Disconnects the input action when destroyed
    /// </summary>
    private void OnDestroy()
    {
        move.performed -= MovePerformed;
        move.canceled -= MoveCanceled;
    }
}
