using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AreaPrompt : MonoBehaviour
{
    public GameEvent EnterAreaPrompt;
    public GameEvent ExitAreaPrompt;
    public Text NewAreaText;
    public string AreaName;

    private float currentDistance;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If colliding with player
        if (collision.gameObject.layer == 8)
        {
            EnterAreaPrompt.Raise();
            NewAreaText.text = AreaName.ToUpper();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // If colliding with player
        if (collision.gameObject.layer == 8) ExitAreaPrompt.Raise();
    }
}
