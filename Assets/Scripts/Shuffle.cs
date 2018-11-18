using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuffle : MonoBehaviour
{
   public static void ShuffleArray<T>(T[] array)
    {
        int p = array.Length;
        for (int i = array.Length - 1; i > 0; i--)
        {
            int r = Random.Range(0, i);
            T tmp = array[i];
            array[i] = array[r];
            array[r] = tmp;
        }
    }
}
