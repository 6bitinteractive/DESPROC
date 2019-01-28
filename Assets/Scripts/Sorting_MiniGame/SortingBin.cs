using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable] public class OnDropIngredient : UnityEvent<Ingredient> { };
[System.Serializable] public class OnVerifyAnswer : UnityEvent<Answer> { };

public enum Answer
{
    Right, Wrong
}

public class SortingBin : MonoBehaviour
{
    public OnDropIngredient OnDropIngredient = new OnDropIngredient();
    public OnVerifyAnswer OnVerifyAnswer = new OnVerifyAnswer();
    public InstructionManager InstructionManager;

    public LayerMask LayerMask;

    private void Start()
    {
        if (InstructionManager == null)
            InstructionManager = FindObjectOfType<InstructionManager>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        int collisionLayerMask = 1 << collision.gameObject.layer;

        // If collides with correct answer
        if (collisionLayerMask == LayerMask.value)
        {
            ScoreManager.EarnScore(100);
            Debug.Log("Nep");
        }
        // else wrong answer
        else
        {
            Debug.Log("NepNep");
        }
    }

    public void CheckIngredient(Ingredient ingredient)
    {
        if (ingredient.Name == InstructionManager.GetCurrentInstruction().correctAnswer)
        {
            Debug.Log(ingredient.Name);
            OnVerifyAnswer.Invoke(Answer.Right);
        }
        else
        {
            Debug.Log("Wrong answer.");
            OnVerifyAnswer.Invoke(Answer.Wrong);
        }

        // Deactivate
        ingredient.gameObject.SetActive(false);
    }
}
