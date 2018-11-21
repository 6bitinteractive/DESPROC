using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SceneDataManager : MonoBehaviour
{
    [SerializeField] protected PlayerDataHandler playerDataHandler;

    protected virtual void Awake()
    {
        if (playerDataHandler.PlayerData == null)
        {
            playerDataHandler.ResetData();
            playerDataHandler.InitializeDataToDefault();
            playerDataHandler.playerSession.UpdateSessionData(playerDataHandler.PlayerData);
        }
        //PlayerData = playerSession.SessionData;
        //if (PlayerData == null)
        //{
        //    Debug.Log("Player data is null; initializing data to default or loading saved data.");
        //    ResetData();
        //    InitializeDataToDefault();
        //    playerSession.UpdateSessionData(PlayerData);
        //}
    }

    protected virtual void Start()
    { }
}
