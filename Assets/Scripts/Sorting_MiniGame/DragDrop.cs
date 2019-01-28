using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDrop : MonoBehaviour
{
    private bool isPressed;
    private bool isReleased;

    public Texture2D Grab;
    public Texture2D Release;

    private SortingBin bowl;

    void Update()
    {
        if (isPressed)
        {
            Drag();
        }
    }

    private void Drag()
    {
        Cursor.SetCursor(Grab, Vector3.zero, CursorMode.Auto);
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // get mouse position
        float distance = Vector2.Distance(mousePosition, transform.position);

        if (isReleased == false)
        {
            transform.position = mousePosition; // Set question's position to mouse position
        }
    }

    private void OnMouseDown()
    {
        isPressed = true;
    }

    private void OnMouseUp()
    {
        isPressed = false;

        // Broadcast which ingredient was dropped
        if (bowl != null)
        {
            bowl.OnDropIngredient.Invoke(GetComponent<Ingredient>());
        }

        Cursor.SetCursor(Release, Vector3.zero, CursorMode.Auto);

        //StartCoroutine(ReleaseBall()); // Release the ball
    }

    //private IEnumerator ReleaseBall()
    //{
    //isReleased = true;
    //// Waits till the ball reaches 1/4 of the slingshot and then releases the ball from the sling shot
    //yield return new WaitForSeconds(releasedDelay);

    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bowl = collision.GetComponent<SortingBin>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        bowl = null;
    }
}