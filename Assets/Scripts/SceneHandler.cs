using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public bool LoadPersistentData;

    void Start()
    {
        if (LoadPersistentData)
        {
            SceneManager.LoadScene("PersistentData", LoadSceneMode.Additive);
        }
    }

    public void ExitScene()
    {
        Application.Quit();
    }

    public void LoadScene(string NextScene)
    {
        SceneManager.LoadScene(NextScene);
    }
}
