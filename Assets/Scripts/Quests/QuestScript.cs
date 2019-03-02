using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestScript : MonoBehaviour
{
    public Quests Quest { get; set; }

    public void Select()
    {
        GetComponent<Text>().color = Color.yellow;
        QuestLog.Instance.DisplayDescription(Quest);
    }

    public void Deselect()
    {

        GetComponent<Text>().color = Color.white;
    }
}
