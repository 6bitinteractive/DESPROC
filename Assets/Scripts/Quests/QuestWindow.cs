using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestWindow : Window
{
    private QuestGiver questGiver;
    [SerializeField] private GameObject questPrefab;
    [SerializeField] private Transform questArea;

    public void DisplayQuests(QuestGiver questGiver)
    {
        this.questGiver = questGiver;

        //Checks if quest giver has already displayed quests
        if (!questGiver.IsDisplayingQuest)
        {
            //Go through each quest from the quest giver
            foreach (Quests quest in questGiver.Quests)
            {
                GameObject gameObject = Instantiate(questPrefab, questArea);
                gameObject.GetComponent<Text>().text = quest.Name; // Display quest name
                questGiver.IsDisplayingQuest = true;
            }
        }     
    }	
}
