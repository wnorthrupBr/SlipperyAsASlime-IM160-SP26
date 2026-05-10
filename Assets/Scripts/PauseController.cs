/*****************************************************************************
// File Name : PauseController.cs
// Author : Will Northrup
// Creation Date : 4/21/2026
//
// Brief Description : This is a script attached to the pause screen manager.
this script allows for the game to be paused by the player, displaying the
pause screen UI.
*****************************************************************************/

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    [SerializeField] private string wantedScene;
    private InputAction pauseGame;
    private bool isPaused;
    [SerializeField] private GameObject pauseScreen;

    /// <summary>
    /// Sets the pause screen to off at start.
    /// </summary>
    void Start()
    {
        pauseScreen.SetActive(false);
        isPaused = false;
        pauseGame = InputSystem.actions.FindAction("PauseGame");
        pauseGame.performed += PauseGamePerformed;
    }

    /// <summary>
    /// when escape is pressed, displays pause screen.
    /// </summary>
    /// <param name="obj"></param>
    private void PauseGamePerformed(InputAction.CallbackContext obj)
    {
        if(!isPaused)
        {
            isPaused = true;
            pauseScreen.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
        }
    }

    /// <summary>
    /// resumes the game when called
    /// </summary>
    public void ResumeGame()
    {
        if(isPaused)
        {
            isPaused = false;
            pauseScreen.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
        }
    }

    /// <summary>
    /// takes player to main menu when called
    /// </summary>
    public void ReturnMainMenu()
    {
        if(isPaused)
        {
            isPaused = false;
            Time.timeScale = 1;
            SceneManager.LoadScene("MainMenuScene");
        }
    }

    /// <summary>
    /// Restarts the level when called
    /// </summary>
    public void RestartLevel()
    {
        if(isPaused)
        {
            isPaused = false;
            Time.timeScale = 1;
            SceneManager.LoadScene(wantedScene);
        }
    }

    /// <summary>
    /// closes the application when called
    /// </summary>
    public void QuitGame()
    {
        if(isPaused)
        {
            isPaused = false;
            Application.Quit();
        }
    }

    /// <summary>
    /// unnbinnds the pause bind when called
    /// </summary>
    private void OnDestroy()
    {
        pauseGame.performed -= PauseGamePerformed;
    }
}
