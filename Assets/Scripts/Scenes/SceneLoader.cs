using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance { get; private set; }

    public void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void LoadScene(int sceneID)
    {
        if (sceneID >= 0 && sceneID < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(sceneID);
        }
        else
        {
            Debug.Log("Scene Transition Unavaliable:" + sceneID);
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
