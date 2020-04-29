using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditScene : MonoBehaviour {

    public CanvasGroup CreditsGroup;
    public float LengthOfFadeOut;
    public UnityEngine.Events.UnityEvent OnEndOfCredits;
    private bool isFadingOut = false;

    private void Start()
    {
        StartCoroutine(CheckIfDone());
    }

    public void StartFadeOut()
    {
        if (isFadingOut) return;
        isFadingOut = true;

        StartCoroutine(FadeOut());
    }

    IEnumerator CheckIfDone()
    {
        Animator anim = GetComponent<Animator>();
        while (true)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("CreditsAnimation") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >=.99f)
            {
                StartFadeOut();
                break;
            }
            yield return null;
        }
    }
	IEnumerator FadeOut()
    {
        float elapsedTime = 0;
        while (CreditsGroup.alpha != 0)
        {
            elapsedTime += Time.deltaTime;

            CreditsGroup.alpha = Mathf.Clamp(1 - (elapsedTime / LengthOfFadeOut),0,1);

            yield return null;
        }
        if(OnEndOfCredits != null)
            OnEndOfCredits.Invoke();
    }
}
