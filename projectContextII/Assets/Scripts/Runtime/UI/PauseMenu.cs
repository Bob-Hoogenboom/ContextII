using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseMenuUI;

    public static bool gameIsPaused = false;

    private void Start()
    {
        pauseMenuUI.SetActive(false);   
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ChangePauseState();
        }
    }

    public void ChangePauseState()
    {
        if (gameIsPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    public void GoToMenu(int menuIndex = 0)
    {
        SceneManager.LoadScene(menuIndex);
    }

    private void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        gameIsPaused = false;
        Camera.main.GetComponent<CinemachineBrain>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; //Unity quirck. cursor doesnt lock right away so we hide it
    }

    private void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        gameIsPaused = true;
        Camera.main.GetComponent<CinemachineBrain>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true; //Unity quirck. Cursor show because we hid it when resuming ^
    }


}
