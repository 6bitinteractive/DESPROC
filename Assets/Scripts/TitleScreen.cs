using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TurtleTale;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] private GameObject ContinueButton;
    [SerializeField] CanvasGroup buttonsCanvasGroup;
    [SerializeField] AudioSource buttonClick;
    [SerializeField] SessionData data;

    private void Start()
    {
        ContinueButton.SetActive(CheckpointSave.instance.HasSaveFile);
    }

    public void LoadPreviousSave()
    {
        buttonClick.Play();
        buttonsCanvasGroup.interactable = false;
        CheckpointSave.instance.LoadGame();
    }

    public void StartNewGame(SceneLoadHandler handler)
    {
        buttonClick.Play();
        buttonsCanvasGroup.interactable = false;
        CheckpointSave.instance.ClearSaveFile();
        data.Reset();

        handler.LoadScene();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
