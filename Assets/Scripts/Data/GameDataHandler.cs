using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameDataHandler : DataHandler
{
    [SerializeField] protected GameData baseGameData;

    [HideInInspector] public GameData GameData;

    protected override void Awake()
    {
        dataFileName = "gameData.json";

        base.Awake();

        GameData = new GameData();
        Utilities.CopyObjectAttributes(baseGameData, GameData);

        GameData = LoadData<GameData>(GameData);
    }

    protected override string GetDataAsJson()
    {
       return JsonUtility.ToJson(GameData);
    }
}
