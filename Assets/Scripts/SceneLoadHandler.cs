using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoadHandler : MonoBehaviour
{
    [SerializeField] private SceneData sceneData;

    private SceneController sceneController;
    private SaveData playerSaveData;

    private void Start()
    {
        sceneController = FindObjectOfType<SceneController>();
        playerSaveData = sceneController.GetPlayerSaveData();
    }

    public void LoadScene()
    {
        playerSaveData.Save(Player.StartingPositionKey, sceneData.StartingPointName);
        sceneController.FadeAndLoadScene(sceneData.SceneName);

    }
}
