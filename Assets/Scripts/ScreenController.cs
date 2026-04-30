/*****************************************************************************
// File Name : ScreenController.cs
// Author : Will Northrup
// Creation Date : 3/24/2026
//
// Brief Description : This is a script that is attached to the SceneManagerGO,
that allows UI buttons to load different scenes and quit the application.
*****************************************************************************/
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ScreenController : MonoBehaviour
{
    /// <summary>
    /// Unlocks mouse at start
    /// </summary>
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    /// <summary>
    /// Loads The first level
    /// </summary>
    public void LoadFirstLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /// <summary>
    /// Quits to desktop
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
}
