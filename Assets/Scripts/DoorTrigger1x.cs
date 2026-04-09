using UnityEngine;

public class DoorTrigger1x : DoorTrigger
{
    /// <summary>
    /// overrides the closing of the door from the parent class.
    /// </summary>
    /// <param name="other"></param>
    public override void OnTriggerExit(Collider other)
    {
        Debug.Log("Dont Close!");
    }
}
