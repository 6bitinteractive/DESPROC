using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientName : MonoBehaviour
{
    private Text ingredientName;

    private void Start()
    {
        ingredientName = GetComponent<Text>();
        ingredientName.text = gameObject.GetComponentInParent<Ingredient>().Name;
    }
}
