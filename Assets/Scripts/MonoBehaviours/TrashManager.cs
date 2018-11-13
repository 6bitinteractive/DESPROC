using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashManager : MonoBehaviour
{
    public static int TotalTrash { get; private set; }

    private void Awake()
    {
        // Check if all children are, indeed, turtles
        int total = transform.childCount;

        for (int i = 0; i < total; i++)
        {
            var trash = transform.GetChild(i).GetComponent<InventoryInteractable>();

            if (trash == null)
                Debug.LogError("TrashManager: Only objects of type InventoryInteractable are allowed to be a child of this transform.");
            else
                TotalTrash = total;
        }
    }
}
