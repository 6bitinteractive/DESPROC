using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    public string NextScene;
    void Update()
    {
        if (Input.anyKeyDown) Invoke("LoadScene", 0.5f);
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(NextScene);
    }
}
