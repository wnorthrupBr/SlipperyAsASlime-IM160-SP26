/*****************************************************************************
// File Name : LaserController.cs
// Author : Will Northrup
// Creation Date : 3/24/2026
//
// Brief Description : This is a script that is used to create lasers using
the line renderer and raycasting. 
*****************************************************************************/
using UnityEngine;

public class LaserController : MonoBehaviour
{
    private LineRenderer lineRenderer;
    [SerializeField] private Transform laserStart;
    [SerializeField] private PlayerLives playerLives;

    /// <summary>
    /// Gets the lineRenderer and playerLives components at start
    /// </summary>
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        playerLives = FindFirstObjectByType<PlayerLives>().GetComponent<PlayerLives>();
    }

    /// <summary>
    /// Manages the raycast/linerenderer's distance and collisions.
    /// </summary>
    void Update()
    {
        //sets the line renderers start position
        lineRenderer.SetPosition(0, laserStart.position);

        RaycastHit hit;

        if (Physics.Raycast(transform.position, -transform.up, out hit))
        {
            //if the raycast hits a collider, it sets the endpoint to the point of collision.
            if (hit.collider)
            {
                lineRenderer.SetPosition(1, hit.point);
            }

            //if the raycast hits the player, the Die() function is called.
            if (hit.transform.CompareTag("Player"))
            {
                playerLives.Die();
            }
        }
        else
        {
            //if the raycast doesnt hit anything, sets a max distance for the line renderer.
            lineRenderer.SetPosition(1, -transform.up * 5000);
        }
    }
}
