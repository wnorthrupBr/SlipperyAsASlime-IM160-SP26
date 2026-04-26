/*****************************************************************************
// File Name : ExitController.cs
// Author : Will Northrup
// Creation Date : 3/24/2026
//
// Brief Description : This is a script that takes the player to the next
// scene when they make contact with the exit game object.
*****************************************************************************/
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitController : MonoBehaviour
{
    [SerializeField] private string wantedScene;

    /// <summary>
    /// loads next scene on contact with exit
    /// </summary>
    /// <param name="collidingObject"></param>
    private void OnTriggerEnter(Collider collidingObject)
    {
        if (collidingObject.transform.CompareTag("Player"))
        {
            //loads the next scene in the Build Profiles->Scene List
            SceneManager.LoadScene(wantedScene);
        }
    }
}
