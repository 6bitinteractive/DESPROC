using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterArea : MonoBehaviour
{
    [SerializeField] private GameEvent OnEnterArea;
    [SerializeField] private string nextScene;
    [SerializeField] private LayerMask playerLayerMask;

    void OnTriggerEnter2D(Collider2D collision)
    {
        int collisionLayerMask = 1 << collision.gameObject.layer;

        // If collides with player display text
        if (collisionLayerMask == playerLayerMask.value)
        {
            //SceneManager.LoadScene(nextScene);
            OnEnterArea.Raise();
        }
    }
}

// Reference: Check layer mask for collision
// http://answers.unity.com/answers/454913/view.html
// https://gamedev.stackexchange.com/questions/119667/how-to-get-the-gameobjects-layermask
