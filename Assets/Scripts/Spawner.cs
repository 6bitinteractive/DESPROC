using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] PooledObject;
    public List<GameObject> PooledObjectList;
    public Transform[] SpawnLocations;
    public float MinimumSpawnRate;
    public float MaximumSpawnRate;
    public int NumberOfObjects = 1;
    private int index;
    private int spawnLocationIndex;

    // Use this for initialization
    void Start ()
    {
        SetObjects();
        StartCoroutine(SpawnRandomObjects());
	}
	
    void SetObjects()
    {
        index = 0;
        spawnLocationIndex = Random.Range(0, SpawnLocations.Length);

        for (int i = 0; i < PooledObject.Length * NumberOfObjects; i++)
        {
            GameObject objects = Instantiate(PooledObject[index], SpawnLocations[spawnLocationIndex].position, Quaternion.identity); // Spawn object prefabs
            PooledObjectList.Add(objects); // Add prefabs to the Pool object list
            PooledObjectList[i].SetActive(false); // Set prefabs as deactivated
            index++;

            // For the sake of multiplying the objects to the number of objects
            if (index == PooledObject.Length)
            {
                index = 0; // Resets index
            }
        }
    }

    IEnumerator SpawnRandomObjects()
    {
        yield return new WaitForSeconds(Random.Range(MinimumSpawnRate, MaximumSpawnRate));

        int i = Random.Range(0, PooledObjectList.Count); // Spawn a random prefab from pooled object list
        spawnLocationIndex = Random.Range(0, SpawnLocations.Length);

        // Checks if pooled object is not active
        if (!PooledObjectList[i].activeInHierarchy)
        {
            PooledObjectList[i].transform.position = SpawnLocations[spawnLocationIndex].position; // Sets spawn location back to originanl position
            PooledObjectList[i].SetActive(true); // Activate the pooled object
            
        }
        StartCoroutine(SpawnRandomObjects()); // Restart the process
    }
}
