/*****************************************************************************
// File Name : CheckpointController.cs
// Author : Will Northrup
// Creation Date : 3/24/2026
//
// Brief Description : This script is used to reset where the player will
respawn after death if a checkpoint is reached.
*****************************************************************************/
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    private bool isActivated;
    private PlayerLives playerLives;

    /// <summary>
    /// gets the playerLives script from the player for referencing at start
    /// </summary>
    void Start()
    {
        playerLives = FindFirstObjectByType<PlayerLives>();
    }

    /// <summary>
    /// if the player touches the checkpoint and it hasn't been
    /// activated, the spawn location will be set to the checkpoint.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            if (!isActivated)
            {
                playerLives.SetSpawnPoint(this.transform.position);
                isActivated = true;
            }
        }
    }
}
