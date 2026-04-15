/*****************************************************************************
// File Name : StickyPlatform.cs
// Author : Will Northrup
// Creation Date : 4/15/2026
//
// Brief Description : This is a script that allows for the player or box
// game objects stick to it when in contact with the platform.
*****************************************************************************/
using UnityEngine;

public class StickyPlatform : MonoBehaviour
{
    /// <summary>
    /// makes the colliding object a child of the platform.
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Box"))
        {
            collision.gameObject.transform.SetParent(transform);
        }
    }

    /// <summary>
    /// orphans the child object when no longer in contact.
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Box"))
        {
            collision.gameObject.transform.SetParent(null);
        }
    }
}
