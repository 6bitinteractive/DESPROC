using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Session/PlayerSession", fileName = "PlayerSession")]
public class PlayerSessionHandler : SessionHandler
{
    [SerializeField] protected PlayerData basePlayerData;
    public PlayerData SessionData;
    public PlayerData StoredSessionData;

    public void InitializeDataToDefault(PlayerDataHandler dataHandler)
    {
        Debug.Log("Initialized player data to default");
        // Copy the data from the base default
        Utilities.CopyObjectAttributes(basePlayerData, dataHandler.PlayerData);
    }

    public void UpdateSessionData(PlayerData data)
    {
        Utilities.CopyObjectAttributes(data, SessionData);
    }

    public void BackupCurrentSession()
    {
        Utilities.CopyObjectAttributes(SessionData, StoredSessionData);
    }

    public void RestorePreviousSession()
    {
        Utilities.CopyObjectAttributes(StoredSessionData, SessionData);
    }
}
