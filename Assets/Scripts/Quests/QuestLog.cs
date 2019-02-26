using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestLog : MonoBehaviour
{
    [SerializeField] private GameObject questPrefab;
    [SerializeField] private Transform questParent;
    [SerializeField] private Text questDescriptionText;

    private Quests selected;
    private static QuestLog instance;

    public static QuestLog Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<QuestLog>();
            }
            return instance;
        }

    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AcceptQuest(Quests quest)
    {
        GameObject gameObject = Instantiate(questPrefab, questParent);
        QuestScript questScript = gameObject.GetComponent<QuestScript>();

        // Assign reference to questscript
        quest.QuestScript = questScript;
        questScript.Quest = quest; 


        gameObject.GetComponent<Text>().text = quest.Name;

    }

    public void DisplayDescription(Quests quest)
    {
        if (selected != null)
        {
            selected.QuestScript.Deselect();
        }

        selected = quest;

        questDescriptionText.text = string.Format("{0}", quest.Name);
    }
}
