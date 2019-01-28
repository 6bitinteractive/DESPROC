using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public GameObject HealthBarSlicePrefab;
    public InstructionManager InstructionManager;
    public Sprite CorrectMark;
    public Sprite IncorrectMark;
    [HideInInspector] public RectTransform RectTransform;
    [HideInInspector] public GridLayoutGroup GridLayoutGroup;
    //public UnityEvent OnCorrectAnswer = new UnityEvent();

    private HealthBarSlice[] healthBarSlices;

    private void Start()
    {
        GridLayoutGroup = GetComponent<GridLayoutGroup>();
        RectTransform = GetComponent<RectTransform>();

        if (InstructionManager == null)
            InstructionManager = FindObjectOfType<InstructionManager>();

        healthBarSlices = new HealthBarSlice[InstructionManager.Instructions.Length];

        for (int i = 0; i < InstructionManager.Instructions.Length; i++)
        {
            GameObject obj = Instantiate(HealthBarSlicePrefab);
            obj.transform.SetParent(transform, false);
            healthBarSlices[i] = obj.GetComponent<HealthBarSlice>();
        }
    }

    public void SetMark(Answer answer)
    {
        if (answer == Answer.Right)
        {
            healthBarSlices[InstructionManager.CurrentIndex].Mark.sprite = CorrectMark;
            //OnCorrectAnswer.Invoke();
        }
        else
        {
            healthBarSlices[InstructionManager.CurrentIndex].Mark.sprite = IncorrectMark;
        }

        InstructionManager.MoveToNextInstruction();
    }

    public void SetPosition()
    {
        RectTransform.localPosition = new Vector3(0, -153, 0);
        //RectTransform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 10));
        GridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        GridLayoutGroup.constraintCount = InstructionManager.Instructions.Length % 2 == 0 ? 4 : 5;
        GridLayoutGroup.childAlignment = TextAnchor.MiddleCenter;
    }
}
