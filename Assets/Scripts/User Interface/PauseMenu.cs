using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        bool isPaused = Time.timeScale == 0;
        pauseMenu.SetActive(!isPaused);
        SetCursor(!isPaused);
    }

    public void PauseScreen()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        SetCursor(true);
        
    }

    public void Continue()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void Retry()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        SceneLoader.Instance.LoadScene(2);
    }

    public void Settings()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        SceneLoader.Instance.LoadScene(1);
    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1;
        SceneLoader.Instance.LoadScene(0);
    }

    public void SetCursor(bool cursor)
    {
        Cursor.lockState = cursor ? CursorLockMode.Confined : CursorLockMode.Locked;
        Cursor.visible = cursor;
    }
}
