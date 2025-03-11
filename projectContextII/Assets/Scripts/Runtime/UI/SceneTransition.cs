using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public void GoToScene(int sceneIndex = 0)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}

