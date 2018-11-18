using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public abstract class DataHandler : MonoBehaviour
{
    protected string dataFileName;
    protected string filePath;

    protected virtual void Awake()
    {
        filePath = Path.Combine(Application.persistentDataPath, dataFileName);
    }

    public virtual T LoadData<T>(T data)
    {
        if (File.Exists(filePath))
        {
            string contents = File.ReadAllText(filePath);

            if (string.IsNullOrEmpty(contents))
            {
                Debug.LogWarning(this + ": JSON data file is empty; returning default data.");
                return data;
            }
            else
            {
                Debug.Log(this + ": Loading JSON data file.");
                JsonUtility.FromJsonOverwrite(contents, data);
                return data;
            }
        }
        else
        {
            Debug.LogWarning(this + ": JSON data file not found; returning default data.");
            return data;
        }
    }

    public virtual void SaveData()
    {
        Debug.Log(this + " JSON data file saved.");
        string dataAsJson = GetDataAsJson();
        File.WriteAllText(filePath, dataAsJson);
    }

    public virtual void ResetData()
    {
        Debug.Log(this + ": Data has been reset.");
    }

    protected abstract string GetDataAsJson();
}
