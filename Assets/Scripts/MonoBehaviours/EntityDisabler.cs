using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityDisabler : MonoBehaviour
{
    public EntityRuntimeSet Set;

    public void RemoveFromList()
    {
        int index = Random.Range(0, Set.Items.Count);
        Set.Remove(Set.Items[index]); // removes from list
        Debug.Log("Removed");
    }

    public void DisableAll()
    {
        // Loop backwards since the list may change when disabling
        for (int i = Set.Items.Count - 1; i >= 0; i--)
        {
            Set.Items[i].gameObject.SetActive(false);
        }
    }

    public void DisableRandom()
    {
        int index = Random.Range(0, Set.Items.Count);
        Set.Items[index].gameObject.SetActive(false);
    }

    public void ChangeSprite(Sprite NewSprite)
    {
        int index = Random.Range(0, Set.Items.Count);

        Set.Items[index].gameObject.GetComponent<SpriteRenderer>().sprite = NewSprite; // Changes to new sprite
        Set.Remove(Set.Items[index]); // removes from list
    }

    public void CheckIfEmpty(GameEvent EventOnEmptyList)
    {
        if (Set.Items.Count <= 1)
        {
            EventOnEmptyList.Raise();
        }
    }
}
