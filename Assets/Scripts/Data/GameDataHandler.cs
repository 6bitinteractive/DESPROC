using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameDataHandler : DataHandler
{
    private GameData gameData;

    protected override void LoadData()
    {
        if (File.Exists(filePath))
        {
            string contents = File.ReadAllText(filePath);

            if (string.IsNullOrEmpty(contents))
            {
                Debug.LogWarning("JSON data file is empty; returning new data.");
                gameData = new GameData();
            }
            else
            {
                Debug.Log("Loading JSON data file.");
                gameData = JsonUtility.FromJson<GameData>(contents);
            }
        }
        else
        {
            Debug.LogWarning("JSON data file not found; returning new data.");
            gameData = new GameData();
        }
    }

    public override void SaveData()
    {
        Debug.Log("JSON data file saved.");
        string dataAsJson = JsonUtility.ToJson(gameData);
        File.WriteAllText(filePath, dataAsJson);
    }
}
