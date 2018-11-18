using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{
    [SerializeField] private PlayerDataHandler playerDataHandler;

    private PlayerData levelData;

    private void Start()
    {
        if (levelData == null)
        {
            Utilities.CopyObjectAttributes(playerDataHandler.PlayerData, levelData);
        }

        levelData = playerDataHandler.LevelPlayerData;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            levelData.MovementSpeed++;
            Debug.Log(levelData.MovementSpeed);
        }
    }
}
