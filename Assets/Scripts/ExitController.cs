using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitController : MonoBehaviour
{
    private void OnTriggerEnter(Collider collidingObject)
    {
        if (collidingObject.gameObject.name == "Player")
        {
            //loads the next scene in the Build Profiles->Scene List
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
