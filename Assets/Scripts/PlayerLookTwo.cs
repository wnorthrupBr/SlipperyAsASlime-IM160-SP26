/*****************************************************************************
// File Name : TurretTracking.cs
// Author : Will Northrup
// Creation Date : 3/24/2026
//
// Brief Description : This is a script that is used to manage the turret/laser
behavior as this script tracks the position of the player and moves the
turret/laser direction to face the player's position.
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

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        move = InputSystem.actions.FindAction("Move");

        move.performed += MovePerformed;
        move.canceled += MoveCanceled;
    }

    private void MoveCanceled(InputAction.CallbackContext obj)
    {
        playerMovement = Vector3.zero;
    }

    private void MovePerformed(InputAction.CallbackContext obj)
    {
        playerMovement.x = obj.ReadValue<Vector2>().x;
        playerMovement.z = obj.ReadValue<Vector2>().y;
    }

    void Update()
    {
        Vector3 viewDir = playerObj.position - new Vector3(transform.position.x, playerObj.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

        Vector3 inputDir = orientation.forward * playerMovement.y + orientation.right * playerMovement.x;

        if (inputDir != Vector3.zero)
        {
            playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
        }
    }

    private void OnDestroy()
    {
        move.performed -= MovePerformed;
        move.canceled -= MoveCanceled;
    }
}
