/*****************************************************************************
// File Name : PlayerLives.cs
// Author : Will Northrup
// Creation Date : 3/24/2026
//
// Brief Description : This is a script that is used to manage the player's 
death state. This includes the disabling of controls, box colliders, and
the rendered player, and applying a few second delay before respawning.
*****************************************************************************/
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLives : MonoBehaviour
{
    [SerializeField] AudioSource deathSound;
    [SerializeField] AudioSource respawnSound;
    [SerializeField] private float reloadDelay;
    [SerializeField] private float lowestYPos;
    private bool isDead;
    [SerializeField] private Vector3 spawnPoint;
    [SerializeField] private Transform startPos;
    private SlimeResize slimeResize;

    /// <summary>
    /// Sets the isDead bool to false and the spawnpoint to 
    /// the player's start position at start.
    /// </summary>
    void Start()
    {
        slimeResize = FindFirstObjectByType<SlimeResize>();
        isDead = false;
        spawnPoint = startPos.position;
    }

    /// <summary>
    /// This is a function that sets the spawnpoint
    /// Vector3 to the checkpoint the player had reached
    /// </summary>
    /// <param name="newSpawnPoint"></param>
    public void SetSpawnPoint(Vector3 newSpawnPoint)
    {
        spawnPoint = newSpawnPoint;
    }

    /// <summary>
    /// if the player collides with an object with the
    /// "EnemyTag" the Die() function is called
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("EnemyTag"))
        {
            Die();
        }
    }

    /// <summary>
    /// This function sets the player to their "dead" state,
    /// and invoking the Respawn function after a delay.
    /// </summary>
    public void Die()
    {
        isDead = true;

        deathSound.Play();

        //player cant move
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<PlayerMove>().enabled = false;

        //player disappears
        GetComponent<MeshRenderer>().enabled = false;

        //respawn player
        Invoke("Respawn", reloadDelay);
    }

    /// <summary>
    /// this function moves the player to their spawnpoint, resetting their size,
    /// and granting control over the character again.
    /// </summary>
    private void Respawn()
    {
        //player can move again
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<PlayerMove>().enabled = true;

        //player reappears
        GetComponent<MeshRenderer>().enabled = true;

        //reset player size
        slimeResize.ResetSlimeScaleAndMass();

        isDead = false;

        //move player to spawnpoint
        this.gameObject.transform.root.position = spawnPoint;

        respawnSound.Play();
    }

    /// <summary>
    /// if the player ever goes below a specific height
    /// in the world, the Die() function is called
    /// </summary>
    void Update()
    {
        if (this.transform.position.y < lowestYPos && !isDead)
        {
            Die();
        }
    }
}
