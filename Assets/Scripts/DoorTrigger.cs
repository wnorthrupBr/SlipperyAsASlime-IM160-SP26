/*****************************************************************************
// File Name : DoorTrigger.cs
// Author : Will Northrup
// Creation Date : 3/24/2026
//
// Brief Description : This is a script that is attached to a Game Object that
will act as a button or trigger for a door. When an object collides with this
trigger, it will open the door. If the trigger is no longer colliding with
anything, the door will close.
*****************************************************************************/
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] GameObject door;
    private bool isOpened;

    /// <summary>
    /// Sets the isOpened bool to false at start;
    /// </summary>
    void Start()
    {
        isOpened = false;
    }

    /// <summary>
    /// if the door is not open, the door will open
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (!isOpened)
        {
            door.transform.position += new Vector3(0, 8, 0);
        }
    }

    /// <summary>
    /// if the trigger is no longer being touched, the door closes.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        door.transform.position += new Vector3(0, -8, 0);
    }
}
