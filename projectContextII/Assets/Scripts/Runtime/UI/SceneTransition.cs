using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{

    [SerializeField] private CircleWipeController circleWipe;

    public void GoToScene(int sceneIndex = 0)
    {
        circleWipe.CircleFadeOut(() =>
        {
            SceneManager.LoadScene(sceneIndex);
            circleWipe.CircleFadeIn();
        });
    }
}

