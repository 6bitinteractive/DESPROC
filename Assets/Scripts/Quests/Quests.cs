using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quests
{
    public QuestScript QuestScript { get; set; }
    public string Name;
    [TextArea(15, 20)]
    public string Description;

     // Use this for initialization
     void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}


// Reference https://www.youtube.com/watch?v=wClMZ2Rim6w