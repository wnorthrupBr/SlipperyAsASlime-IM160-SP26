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

public class TurretTracking : MonoBehaviour
{
    [SerializeField] private float trackingSpeed;
    [SerializeField] private GameObject target;
    private Vector3 lastKnownPosition;
    private Quaternion lookAtRotation;


    /// <summary>
    /// Sets the lastKnownPosition Vector3 to zero at start
    /// </summary>
    void Start()
    {
        lastKnownPosition = Vector3.zero;
    }

    /// <summary>
    /// every frame the player's position is updated and the 
    /// turret/laser will rotate towards that position
    /// </summary>
    void Update()
    {
        //if the last player position is not the same as the current
        //player position, the last player position is set to current player position
        if (lastKnownPosition != target.transform.position)
        {
            lastKnownPosition = target.transform.position;
            lookAtRotation = Quaternion.LookRotation(lastKnownPosition - transform.position);
        }

        //if the turret/laser is not facing the player,
        //it will rotate towards the last player position
        if (transform.rotation != lookAtRotation)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookAtRotation, 
                trackingSpeed * Time.deltaTime);
        }
    }
}
