using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurtleTale
{
    [CreateAssetMenu(menuName = "Persistent Data/QuestGiver Data", fileName = "QuestGiverData")]
    public class QuestGiverData : ScriptableObject
    {
        public string QuestGiverName;
        private bool isQuestSet = false;
        public List<Quests> QuestGiverQuests;

        public bool IsQuestSet
        {
            get
            {
                return isQuestSet;
            }

            set
            {
                isQuestSet = value;
            }
        }

        private void OnEnable()
        {
            Reset();
        }

        public void Initialize(bool isQuestSet)
        {
            this.IsQuestSet = isQuestSet;
        }

        public static QuestGiverData CreateInstance(bool isQuestSet)
        {
            var questGiverData = CreateInstance<QuestGiverData>();
            questGiverData.Initialize(isQuestSet);
            return questGiverData;
        }

        public void Reset()
        {
            IsQuestSet = false;
            QuestGiverQuests = new List<Quests>();
        }
    }
}