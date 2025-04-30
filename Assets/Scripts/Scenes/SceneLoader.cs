using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
   public void LoadScene( int sceneID)
    {
        if(sceneID >= 0 && sceneID < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(sceneID);
        }
        else
        {
            Debug.Log("Scene Transition Unavaliable:" + sceneID);
        }
    }
}
