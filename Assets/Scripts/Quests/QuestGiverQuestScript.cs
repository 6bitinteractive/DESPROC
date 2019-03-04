using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiverQuestScript : MonoBehaviour
{
    public Quests Quest { get; set; }

    public void Select()
    {
        QuestWindow.Instance.DisplayQuestInfo(Quest);
    }
}
