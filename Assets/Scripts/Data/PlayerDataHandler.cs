using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// NOTE: Player data might best be saved as a binary file and stored at the persistentData path?

public class PlayerDataHandler : DataHandler
{
    [SerializeField] private PlayerData playerData;

    protected override void Start()
    {
        base.Start();

        //filePath = Path.Combine(Application.persistentDataPath, dataFileName);
    }

    protected override void LoadData()
    {
        if (File.Exists(filePath))
        {
            string contents = File.ReadAllText(filePath);

            if (string.IsNullOrEmpty(contents))
            {
                Debug.LogWarning("JSON data file is empty; returning new data.");
                playerData = new PlayerData();
            }
            else
            {
                Debug.Log("Loading JSON data file.");
                playerData = JsonUtility.FromJson<PlayerData>(contents);
            }
        }
        else
        {
            Debug.LogWarning("JSON data file not found; returning new data.");
            playerData = new PlayerData();
        }
    }

    public override void SaveData()
    {
        Debug.Log("JSON data file saved.");
        string dataAsJson = JsonUtility.ToJson(playerData);
        File.WriteAllText(filePath, dataAsJson);
    }
}
