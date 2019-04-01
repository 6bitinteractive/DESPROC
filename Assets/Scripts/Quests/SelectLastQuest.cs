using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectLastQuest : MonoBehaviour
{
    [SerializeField] Scrollbar scrollbar;
    private List<Button> buttons = new List<Button>();

    private void OnEnable()
    {
        SelectLast();
    }

    public void SelectLast()
    {
        buttons.Clear();

        // Get all the quest buttons
        buttons.AddRange(GetComponentsInChildren<Button>());

        if (buttons.Count > 0)
        {
            // Get only the buttons that are actually enabled
            // This assumes that the current setup for quests still holds true--that the button with the QuestScript component is disabled
            buttons = buttons.FindAll(x => x.GetComponent<Button>().enabled);

            // Get the last quest
            Button b = buttons[buttons.Count - 1];
            b.Select(); // Select (Unity) the button
            b.onClick.Invoke(); // Invoke the onClick event

            StartCoroutine(ScrollToBottom());
        }
    }

    private IEnumerator ScrollToBottom()
    {
        yield return new WaitForEndOfFrame();

        // Scroll to bottom
        scrollbar.value = 0;
    }
}
