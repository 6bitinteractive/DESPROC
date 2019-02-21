using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : MonoBehaviour
{
    public float startWaitTime;
    public Transform MoveArea;
    public float MinX;
    public float MaxX;
    public float MinY;
    public float MaxY;

    private float waitTime;

	// Use this for initialization
	void Start ()
    {
        waitTime = startWaitTime;
        FindNextPoint();

    }

    public void WanderToNewPoint(float Speed)
    {
        transform.position = Vector2.MoveTowards(transform.position, MoveArea.position, Speed * Time.deltaTime);

        // If entity is nearing target position
        if (Vector2.Distance(transform.position, MoveArea.position) < 0.2f)
        {
            // Wait a bit
            if (waitTime <= 0)
            {
                FindNextPoint();
                waitTime = startWaitTime; // Reset waittimer
            }

            // Reduce wait time
            else waitTime -= Time.deltaTime;
        }
    }

    private void FindNextPoint()
    {
        // Set new target
        MoveArea.position = new Vector2(Random.Range(MinX, MaxX), Random.Range(MinY, MaxY));

        // Rotate Towards new Target
        Vector2 direction = MoveArea.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

    }
}
