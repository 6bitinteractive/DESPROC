using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public const string StartingPositionKey = "Starting Position";

    [SerializeField] private SaveData playerSaveData;

    private void Start()
    {
        SetStartingPosition();
        CheckpointSave.instance.SaveGame();
    }

    private void SetStartingPosition()
    {
        string startingPositionName = "";
        playerSaveData.Load(StartingPositionKey, ref startingPositionName);
        Transform startingPosition = StartingPosition.FindStartingPosition(startingPositionName);
        
        if(startingPosition != null)
            transform.position = startingPosition.position;
    }
}
