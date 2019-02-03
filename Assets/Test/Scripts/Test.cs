using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private GameEvent earnMoneyEvent;
    [SerializeField] private SceneData sceneData;
    [SerializeField] private SaveData playerSaveData;

    private SceneController sceneController;

    private void Start()
    {
        sceneController = FindObjectOfType<SceneController>(); // Don't want this? Build a dependency injection framework
    }

    public void RaiseEarnMoneyEvent()
    {
        earnMoneyEvent.Raise();
    }

    public void LoadScene()
    {
        playerSaveData.Save(Player.StartingPositionKey, sceneData.StartingPointName);
        sceneController.FadeAndLoadScene(sceneData.SceneName);
    }
}
