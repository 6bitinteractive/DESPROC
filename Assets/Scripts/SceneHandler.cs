using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{

    public void ExitScene()
    {
        Application.Quit();
    }

    public void LoadScene(string NextScene)
    {
        SceneManager.LoadScene(NextScene);
        SceneManager.LoadSceneAsync("PersistentData", LoadSceneMode.Additive);
    }

    public void LoadPersistentData()
    {
        SceneManager.LoadScene("PersistentData", LoadSceneMode.Additive);
    }
}
