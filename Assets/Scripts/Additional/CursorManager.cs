using UnityEngine;
using UnityEngine.SceneManagement;

public class CursorManager : MonoBehaviour
{
    public bool autoDetectScenes = true;

    public int[] menuScenesIndexes;

    private void Start()
    {
        if (autoDetectScenes)
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

            if (IsMenuScene(currentSceneIndex)) {
                ShowCursor();
            }
            else
            {
                HideCursor();
            }
        }
    }

    void ShowCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void HideCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    bool IsMenuScene(int sceneIndex)
    {
        foreach (int menuIndex in menuScenesIndexes)
        {
            if (sceneIndex == menuIndex)
            {
                return true;
            }
        }
        return false;
    }

    public void SetCursorVisible(bool visible)
    {
        Cursor.visible = visible;
        Cursor.lockState = visible ? CursorLockMode.None : CursorLockMode.Locked;
    }
}
