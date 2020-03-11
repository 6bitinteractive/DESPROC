using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TurtleTale;
using UnityEngine.SceneManagement;

public class CheckpointSave : MonoBehaviour {

    const string Quests = "QuestSaveKey";
    const string PlasticsCollected = "PlasticsCollectedKey";
    const string PlasticsSorted = "PlasticsSortedKey";
    const string StartScenePos = "ScenePosKey";
    const string SceneName = "SceneNameKey";
    const string QuestGiver = "QuestGiverKey";

    public static CheckpointSave instance;
    [SerializeField] private SaveData PlayerSaveData;
    [SerializeField] private SessionData CurrentSessionData;

    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(this);
    }

    public void SaveGame()
    {
        string startingPosValue = "";
        PlayerSaveData.Load(Player.StartingPositionKey, ref startingPosValue);
        PlayerPrefs.SetString(StartScenePos, startingPosValue);
        Debug.Log(startingPosValue);
        PlayerPrefs.SetString(SceneName, SceneManager.GetActiveScene().name);

        PlayerPrefs.SetString(Quests, JsonHelper.ToJson(CurrentSessionData.Quests.ToArray()));
        SavePlastic(CurrentSessionData.CollectedPlastic, PlasticsCollected);
        SavePlastic(CurrentSessionData.SortedPlastic, PlasticsSorted);
    }

    public void LoadGame()
    {
        SceneController cont = FindObjectOfType<SceneController>();
        cont.FadeAndLoadScene(PlayerPrefs.GetString(SceneName));
        string startingPosValue = PlayerPrefs.GetString(StartScenePos);
        PlayerSaveData.Save(Player.StartingPositionKey,startingPosValue);

        CurrentSessionData.Reset();

        Quests[] data = JsonHelper.FromJson<Quests>(PlayerPrefs.GetString(Quests));
        CurrentSessionData.Quests = new List<Quests>(data);
        LoadPlastic(PlasticsCollected, ref CurrentSessionData.CollectedPlastic);
        LoadPlastic(PlasticsSorted, ref CurrentSessionData.SortedPlastic);
    }

    private void SavePlastic(List<PlasticData> data, string key)
    {
        List<string> resourceName = new List<string>();
        foreach (PlasticData d in data) resourceName.Add(d.name);
        string a = JsonHelper.ToJson(resourceName.ToArray());
        PlayerPrefs.SetString(key, a);
    }

    private void LoadPlastic(string key, ref List<PlasticData> data)
    {
        string[] giverData = JsonHelper.FromJson<string>(PlayerPrefs.GetString(key));
        data = new List<PlasticData>();
        foreach (string str in giverData)
        {
            data.Add((PlasticData)Resources.Load("Main/Plastic/" + str));
        }
    }

    public void ClearSaveFile()
    {
        PlayerPrefs.DeleteKey(Quests);
        PlayerPrefs.DeleteKey(PlasticsCollected);
        PlayerPrefs.DeleteKey(PlasticsSorted);
        PlayerPrefs.DeleteKey(StartScenePos);
        PlayerPrefs.DeleteKey(SceneName);
        PlayerPrefs.DeleteKey(QuestGiver);
    }

    public bool HasSaveFile()
    {
        return PlayerPrefs.HasKey(Quests);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveGame();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadGame();
        }
    }
}

public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [SerializeField]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}