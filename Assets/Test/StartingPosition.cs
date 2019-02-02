using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingPosition : MonoBehaviour
{
    [SerializeField] private SceneData sceneData;

    private static List<StartingPosition> allStartingPositions = new List<StartingPosition>();

    private void OnEnable()
    {
        allStartingPositions.Add(this);
    }

    private void OnDisable()
    {
        allStartingPositions.Remove(this);
    }

    public static Transform FindStartingPosition(string pointName)
    {
        for (int i = 0; i < allStartingPositions.Count; i++)
        {
            if (allStartingPositions[i].sceneData.StartingPointName == pointName)
                return allStartingPositions[i].transform;
        }

        return null;
    }
}

// Reference: https://unity3d.com/learn/tutorials/projects/adventure-game-tutorial
