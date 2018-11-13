using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Interactable : MonoBehaviour
{
    public InteractableObject Base;

    private Image Image;

    private void Awake()
    {
        Image = GetComponent<Image>();
    }

    private void Start()
    {
        Image.sprite = Base.Sprite;
    }
}
