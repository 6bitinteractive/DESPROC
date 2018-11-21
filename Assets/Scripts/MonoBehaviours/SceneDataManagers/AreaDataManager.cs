using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDataManager : SceneDataManager
{
    // Make changes to session data
    // OnGameOver: revert back to previous session data
    // else: copy session to playerdata

    protected override void Start()
    {
        Debug.Log("Entered an area of the level.");
        playerDataHandler.PlayerData = playerDataHandler.playerSession.SessionData;
    }
}
