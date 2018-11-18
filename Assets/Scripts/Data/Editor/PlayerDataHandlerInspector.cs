using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.IO;

[CustomEditor(typeof(PlayerDataHandler))]
public class PlayerDataHandlerInspector : Editor
{
    private PlayerData playerData;
    //private string gameDataProjectFilePath = "/StreamingAssets/playerData.json";
    private string fileName = "playerData.json";
    private string filePath;

    //SerializedProperty fileNameProperty;
    //SerializedProperty playerDataProperty;

    private void OnEnable()
    {
        filePath = Path.Combine(Application.persistentDataPath, fileName);
        //filePath = Application.dataPath + gameDataProjectFilePath;
        //fileNameProperty = serializedObject.FindProperty("dataFileName");
        //playerDataProperty = serializedObject.FindProperty("playerData");

        PlayerDataHandler dataHandler = (PlayerDataHandler)target;
        playerData = dataHandler.PlayerData;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI(); // keep default inspector

        serializedObject.Update();

        //EditorGUILayout.PropertyField(fileNameProperty);
        //EditorGUILayout.PropertyField(playerDataProperty, true);

        serializedObject.ApplyModifiedProperties();

        //if (GUILayout.Button("Save data"))
        //{
        //    SavePlayerData();
        //}

        //if (GUILayout.Button("Load data"))
        //{
        //    LoadPlayerData();
        //}
    }

    //private void LoadPlayerData()
    //{
    //    if (File.Exists(filePath))
    //    {
    //        Debug.Log("filefae");
    //        string dataAsJson = File.ReadAllText(filePath);
    //        playerData = JsonUtility.FromJson<PlayerData>(dataAsJson);
    //    }
    //    else
    //    {
    //        playerData = new PlayerData();

    //        //PlayerDataHandler dataHandler = (PlayerDataHandler)target;
    //        //playerData = dataHandler.playerData;
    //    }
    //}

    private void SavePlayerData()
    {
        //Debug.LogWarning("Make sure the path is the same one set in the PlayerDataHandler class.");
        string dataAsJson = JsonUtility.ToJson(playerData);
        File.WriteAllText(filePath, dataAsJson);
    }
}

// Reference: https://docs.unity3d.com/ScriptReference/Editor.html
