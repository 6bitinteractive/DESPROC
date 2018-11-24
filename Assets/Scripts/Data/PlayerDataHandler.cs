using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// NOTE: Player data might best be saved as a binary file and stored at the persistentData path?

[DisallowMultipleComponent]
public class PlayerDataHandler : DataHandler
{
    public PlayerData playerData;

    protected override void Awake()
    {
        dataFileName = "playerData.json";

        base.Awake();
        //filePath = Path.Combine(Application.persistentDataPath, dataFileName);
    }

    public override void LoadData()
    {
        if (File.Exists(filePath))
        {
            string contents = File.ReadAllText(filePath);

            if (string.IsNullOrEmpty(contents))
            {
                Debug.LogWarning(this + " JSON data file is empty; returning new data.");
                playerData = new PlayerData();
            }
            else
            {
                Debug.Log(this + " Loading JSON data file.");
                playerData = JsonUtility.FromJson<PlayerData>(contents);
            }
        }
        else
        {
            Debug.LogWarning(this + " JSON data file not found; returning new data.");
            playerData = new PlayerData();
        }
    }

    public override void SaveData()
    {
        Debug.Log(this + " JSON data file saved.");
        string dataAsJson = JsonUtility.ToJson(playerData);
        File.WriteAllText(filePath, dataAsJson);
    }

    public void ResetData()
    {
        playerData = new PlayerData();
        SaveData();
    }
}
