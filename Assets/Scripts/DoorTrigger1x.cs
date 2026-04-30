/*****************************************************************************
// File Name : DoorTrigger1x.cs
// Author : Will Northrup
// Creation Date : 4/15/2026
//
// Brief Description : This is a script that changes how the original
// DoorTrigger.cs scriot functions for different interactions.
*****************************************************************************/
using UnityEngine;

public class DoorTrigger1x : DoorTrigger
{
    /// <summary>
    /// overrides the closing of the door from the parent class.
    /// </summary>
    /// <param name="other"></param>
    public override void OnTriggerExit(Collider other)
    {
        this.gameObject.SetActive(false);
        Debug.Log("Dont Close!");
    }
}
