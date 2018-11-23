using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActiveSceneUnloader : MonoBehaviour
{
    public void UnloadScene()
    {
        Debug.Log("Unloaded: " + SceneManager.GetActiveScene().name);
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
    }
}
