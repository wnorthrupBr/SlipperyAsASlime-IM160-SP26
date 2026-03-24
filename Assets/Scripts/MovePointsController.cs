/*****************************************************************************
// File Name : MovePointsController.cs
// Author : Will Northrup
// Creation Date : 3/24/2026
//
// Brief Description : This is a script that can be applied to many objects.
This script moves the object attached to it between various points set up in
the scene.
*****************************************************************************/
using UnityEngine;

public class MovePointsController : MonoBehaviour
{
    [SerializeField] private GameObject[] movePoints;
    [SerializeField] private float speed;
    private int currentIndex;

    /// <summary>
    /// Sets the currentIndex to 0 at start
    /// </summary>
    void Start()
    {
        currentIndex = 0;
    }

    /// <summary>
    /// Every frame the current target location of the object is 
    /// checked/changed and the object is moved towards that target location
    /// </summary>
    void Update()
    {
        //if the distance between the object's position and the
        //target location is small enough, the currentIndex is increased
        if (Vector3.Distance(transform.position, movePoints[currentIndex].transform.position) < 0.1f)
        {
            currentIndex++;

            //if current index exceeds the max amount of points
            //available, the current index is set to 0
            if (currentIndex >= movePoints.Length)
            {
                currentIndex = 0;
            }
        }

        //moves the object towards the current target position.
        transform.position = Vector3.MoveTowards(transform.position, movePoints[currentIndex].transform.position,
            speed * Time.deltaTime);
    }
}
