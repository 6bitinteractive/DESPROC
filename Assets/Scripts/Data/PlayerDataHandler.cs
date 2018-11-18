using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerDataHandler : DataHandler
{
    [SerializeField] protected PlayerData basePlayerData;

    [Tooltip("Player data for the current session.")]
    public PlayerData PlayerData;

    public PlayerData LevelPlayerData;

    protected override void Awake()
    {
        // Change the dataFileName first
        dataFileName = "playerData.json";

        base.Awake();

        Utilities.CopyObjectAttributes(basePlayerData, PlayerData);

        // Overwrite the data if there's a saved file, else keep the default
        PlayerData = LoadData<PlayerData>(PlayerData);
    }

    protected override string GetDataAsJson()
    {
        return JsonUtility.ToJson(PlayerData);
    }

    public override void ResetData()
    {
        PlayerData = ScriptableObject.CreateInstance<PlayerData>();
        base.ResetData();
    }
}

