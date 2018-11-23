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
        filePath = Path.Combine(Application.streamingAssetsPath, dataFileName);
        LoadData();
    }

    public abstract void LoadData();
    public abstract void SaveData();
}
