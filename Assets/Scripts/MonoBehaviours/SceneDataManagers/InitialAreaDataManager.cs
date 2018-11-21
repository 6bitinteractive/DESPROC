using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialAreaDataManager : AreaDataManager
{
    // Backup current session <- the only difference
    // Same as regular area... make changes to session data, etc

    protected override void Start()
    {
        Debug.Log("Entered first area of the level.");

        playerDataHandler.playerSession.BackupCurrentSession();

        base.Start();
    }
}
