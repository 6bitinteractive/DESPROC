using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDataManager : SceneDataManager
{
    // Load  session to player data

    protected override void Start()
    {
        playerDataHandler.PlayerData = playerDataHandler.playerSession.SessionData;
    }
}
