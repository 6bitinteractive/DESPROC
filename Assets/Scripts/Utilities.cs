using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities
{
    // Copy public attributes from one object to another (?)
    public static void CopyObjectAttributes(object copyFrom, object copyTo)
    {
        JsonUtility.FromJsonOverwrite(JsonUtility.ToJson(copyFrom), copyTo);
    }
}

// Reference: http://answers.unity.com/answers/1261070/view.html
// |_ Hack to do a deep copy of the values of another object, i.e. copy by value not reference
