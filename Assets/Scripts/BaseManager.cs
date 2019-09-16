using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseManager<T> : MonoBehaviour where T: MonoBehaviour
{
    //If you are going to restart the game, please manually reset this as well. Static values will retain its value through entire session
    private static bool m_isShuttingDown = false;
    public static bool IsShuttingDown { get { return m_isShuttingDown; } }
    private static T m_instance;

    public static T Instance
    {
        get
        {
            //Dont get if shutting down
            if (m_isShuttingDown)
            {
                Debug.LogErrorFormat("{0} is now shutting down, now returning null", typeof(T));
                return null;
            }

            //If no instance set, find it first
            if (m_instance == null)
                m_instance = (T)FindObjectOfType(typeof(T));

            //if still not found, spit error
            if (m_instance == null)
                Debug.LogErrorFormat("{0} not found in scene!", typeof(T));

            return m_instance;
        }
    }

    private void OnApplicationQuit()
    {
        m_isShuttingDown = true;
    }

    private void OnDestroy()
    {
        m_isShuttingDown = true;
    }

    protected virtual void Start()
    {
        m_isShuttingDown = false;
    }
}
