using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public string NextScene;

    public void ExitScene()
    {
        Application.Quit();
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(NextScene);
        SceneManager.LoadSceneAsync("PersistentData", LoadSceneMode.Additive);
    }

    public void LoadPersistentData()
    {
        SceneManager.LoadScene("PersistentData", LoadSceneMode.Additive);
    }
}
