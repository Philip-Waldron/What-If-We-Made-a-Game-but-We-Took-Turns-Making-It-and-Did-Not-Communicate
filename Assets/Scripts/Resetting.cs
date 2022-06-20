using UnityEngine;
using UnityEngine.SceneManagement;

public class Resetting : MonoBehaviour
{
    void Update()
    {
//        if (Input.GetKeyDown(KeyCode.Escape))
//        {
//            Application.Quit();
//        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }
}
