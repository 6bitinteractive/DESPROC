using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterArea : MonoBehaviour
{
    public string NextScene;

    void OnTriggerEnter2D(Collider2D collision)
    {
        // If collides with player display text
        if (collision.gameObject.layer == 8)
        {
            SceneManager.LoadScene(NextScene);
        }
    }
}
