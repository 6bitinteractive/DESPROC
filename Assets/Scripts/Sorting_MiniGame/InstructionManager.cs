using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InstructionManager : MonoBehaviour
{
    public UnityEvent OnInstructionEnd = new UnityEvent();
    public Text InstructionText;
    public Instruction[] Instructions;
    public float EndPanelPopupDelay = 0f;

    [HideInInspector] public int CurrentIndex;

    private void Start()
    {
        SetTextInstruction();
    }

    public void SetTextInstruction()
    {
        InstructionText.text = Instructions[CurrentIndex].text;
    }

    public void MoveToNextInstruction()
    {
        CurrentIndex++;

        if (CurrentIndex >= Instructions.Length)
        {
            CurrentIndex = 0;
            Invoke("EndLevel", EndPanelPopupDelay);
        }
        else
        {
            SetTextInstruction();
        }
    }

    public Instruction GetCurrentInstruction()
    {
        return Instructions[CurrentIndex];
    }

    public void EndLevel()
    {
        OnInstructionEnd.Invoke();
    }
}
