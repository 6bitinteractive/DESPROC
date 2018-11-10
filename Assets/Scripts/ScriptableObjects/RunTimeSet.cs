using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Reference https://youtu.be/raQ3iHhE_Kk?t=1964

 public abstract class RuntimeSet<T> : ScriptableObject
 {
    public List<T> Items = new List<T>();

    public void Add(T entity)
    {
        if (!Items.Contains(entity)) Items.Add(entity);
    }

    public void Remove(T entity)
    {
        if (Items.Contains(entity)) Items.Remove(entity);
    }
 }

