using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]

public class SpawnArea : MonoBehaviour
{
    public GameObject TrashPrefab;
    public float SpawnRate = 1.0f;
    public int InitialItems = 5;

    private Vector3 spawnAreaMin;
    private Vector3 spawnAreaMax;

    private void Start()
    {
        GetBounds();

        for (int i = 0; i < InitialItems; i++)
            SpawnTrash();

        InvokeRepeating("SpawnTrash", 1f, SpawnRate);
    }

    private void SpawnTrash()
    {
        float x = UnityEngine.Random.Range(spawnAreaMin.x, spawnAreaMax.x);
        float y = UnityEngine.Random.Range(spawnAreaMin.y, spawnAreaMax.y);
        Vector2 position = new Vector2(x, y);

        GameObject spawnedObj = Instantiate(TrashPrefab, position, Quaternion.identity);
        RectTransform rect = spawnedObj.GetComponent<RectTransform>();
        rect.anchoredPosition = (position * 100f); // HACK: multiplying it by 100 because objects were too close with each other
        spawnedObj.transform.SetParent(transform, false);
    }

    private void GetBounds()
    {
        RectTransform rect = GetComponent<RectTransform>();
        Vector3[] v = new Vector3[4];
        rect.GetWorldCorners(v);

        // HACK: Subtract rect.position so that objects are not offset
        spawnAreaMin = v[0] - (Vector3)rect.position;
        spawnAreaMax = v[2] - (Vector3)rect.position;
    }
}
