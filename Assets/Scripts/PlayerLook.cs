/*****************************************************************************
// File Name : PlayerLook.cs
// Author : Will Northrup
// Creation Date : 3/24/2026
//
// Brief Description : This is a script that is attached to the camera. This
script allows for the player to look around using the mouse, and having the
movement be relative to the player looking direction.
*****************************************************************************/
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private PlayerMove move;
    [SerializeField] private InputAction mouseLook;
    private Vector2 mouseLookData;
    [SerializeField] private float minAngle;
    [SerializeField] private float maxAngle;

    /// <summary>
    /// sets the movement of mouse input and camera start location. Locks mouse at center of screen
    /// </summary>
    void Start()
    {
        if (move != null)
        {
            move = FindFirstObjectByType<PlayerMove>();
        }
        
        mouseLook = InputSystem.actions.FindAction("Look");

        mouseLook.performed += MouseLookPerformed;

        Cursor.lockState = CursorLockMode.Locked;

        transform.localEulerAngles = new Vector3(0, 0, 0);
    }

    /// <summary>
    /// This is a function that is made to clamp the camera from going past specific angles
    /// </summary>
    /// <param name="angle"></param>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static float ClampAngle(float angle, float min, float max)
    {
        float start = (min + max) * 0.5f - 180;
        float floor = Mathf.FloorToInt((angle - start) / 360) * 360;
        return Mathf.Clamp(angle, min + floor, max + floor);
    }

    /// <summary>
    /// When the mouse is moved, the camera will tilt up and down, and the player is rotated left and right
    /// based on direction moved
    /// </summary>
    /// <param name="obj"></param>
    private void MouseLookPerformed(InputAction.CallbackContext obj)
    {
        mouseLookData = obj.ReadValue<Vector2>();

        move.transform.Rotate(Vector3.up, mouseLookData.x * 0.25f, Space.Self);

        transform.Rotate(Vector3.left, mouseLookData.y * 0.25f, Space.Self);

        transform.localEulerAngles = new Vector3(ClampAngle(transform.localEulerAngles.x, minAngle, maxAngle), 0, 0);
    }

    /// <summary>
    /// Disconnects the mouse input action when destroyed
    /// </summary>
    private void OnDestroy()
    {
        mouseLook.performed -= MouseLookPerformed;
    }
}
