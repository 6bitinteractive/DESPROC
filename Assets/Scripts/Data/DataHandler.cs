using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public abstract class DataHandler : MonoBehaviour
{
    [Tooltip("Make sure to rename to a unique filename.")]
    [SerializeField] protected string dataFileName = "Data.json";

    protected string filePath;

    protected virtual void Start()
    {
        filePath = Path.Combine(Application.streamingAssetsPath, dataFileName);
        LoadData();
    }

    protected abstract void LoadData();
    public abstract void SaveData();
}
