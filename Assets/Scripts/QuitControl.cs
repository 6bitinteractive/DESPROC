using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitControl : MonoBehaviour {

    private bool IsQuitting = false;
    private Canvas canvas;
    public bool CanQuit = true;
    public UnityEngine.UI.Text MainText;
    public UnityEngine.UI.Text AcceptText;

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

            if (IsQuitting)
            {
                Time.timeScale = 0;

                if(SceneManager.GetActiveScene().name == "TitleScreen")
                {
                    MainText.text = "Quit Game?";
                    AcceptText.text = "Quit";
                }
                else
                {
                    MainText.text = "Return to Title Screen?";
                    AcceptText.text = "Return";
                }
            }

            else CancelQuit();
        }
	}

    public void QuitGame()
    {
        if (SceneManager.GetActiveScene().name == "TitleScreen")
            Application.Quit();
        else
        {
            GetComponentInChildren<SceneLoadHandler>().LoadScene();
            CancelQuit();
        }
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
