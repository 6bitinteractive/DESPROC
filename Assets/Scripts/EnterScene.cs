using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterScene : MonoBehaviour
{
    public GameEvent OnLoadScene;

    void Start ()
    {
        OnLoadScene.Raise();
	}
}
