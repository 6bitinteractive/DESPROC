using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerDataHandler : DataHandler
{
    [SerializeField] protected PlayerSessionHandler playerSession;
    [HideInInspector] public PlayerData PlayerData;

    protected override void Awake()
    {
        // Change the dataFileName first
        dataFileName = "playerData.json";

        base.Awake();

        //PlayerData = playerSession.SessionData;
        if (PlayerData == null)
        {
            Debug.Log("Player data is null; initializing data to default or loading saved data.");
            ResetData();
            InitializeDataToDefault();
            playerSession.UpdateSessionData(PlayerData);
        }
    }

    public void LoadSavedData()
    {
        LoadData<PlayerData>(PlayerData);
    }

    public void InitializeDataToDefault()
    {
        playerSession.InitializeDataToDefault(PlayerData);
    }

    protected override string GetDataAsJson()
    {
        return JsonUtility.ToJson(PlayerData);
    }

    public override void ResetData()
    {
        base.ResetData();
        PlayerData = ScriptableObject.CreateInstance<PlayerData>();
    }
}
