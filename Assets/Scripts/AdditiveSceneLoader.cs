using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdditiveSceneLoader : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        StartCoroutine(SwitchScene(sceneName));
    }

    private IEnumerator SwitchScene(string sceneName)
    {
        AsyncOperation load = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        yield return load; // wait until new scene has been loaded

        Scene scene = SceneManager.GetSceneByName(sceneName);
        SceneManager.SetActiveScene(scene); // set the newly loaded scene as the active scene; see reference below
        Debug.Log("Current active scene: " + SceneManager.GetActiveScene().name);
    }
}

// Reference
// First scene is still the active scene when loading additively
// https://docs.unity3d.com/ScriptReference/SceneManagement.SceneManager.SetActiveScene.html
