using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitControl : MonoBehaviour {

    private bool IsQuitting = false;
    private Canvas canvas;
    public bool CanQuit = true;

    private AudioSource audioSource;
    private void Awake()
    {
        SceneController.BeforeFade += () => { CanQuit = false; Debug.Log("BeforeSceneUnload"); };
        SceneController.AfterFade += () => CanQuit = true;
    }

    // Use this for initialization
    void Start () {
        canvas = this.GetComponent<Canvas>();
        audioSource = this.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {

        if (!CanQuit) return;

		if(Input.GetKeyDown(KeyCode.Escape))
        {
            audioSource.Play();
            IsQuitting = !IsQuitting;

            canvas.enabled = IsQuitting;

            if (IsQuitting) Time.timeScale = 0;

            else CancelQuit();
        }
	}

    public void QuitGame()
    {
        Application.Quit();
    }

    public void CancelQuit()
    {
        Time.timeScale = 1;
        audioSource.Play();
        IsQuitting = false;
        canvas.enabled = false;
    }

    public void SetCanQuit(bool enabled) { CanQuit = enabled; }
}
