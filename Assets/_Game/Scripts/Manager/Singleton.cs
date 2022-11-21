using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T:MonoBehaviour
{
    private static T m_ins;
    public static T Ins
    {
        get
        {
            if (m_ins == null)
            {
                m_ins = GameObject.FindObjectOfType<T>();
                
                if(m_ins == null)
                {
                    GameObject singletonObject = new GameObject();
                    m_ins = singletonObject.AddComponent<T>();
                    singletonObject.name = typeof(T).ToString() + " (Singleton)";
                }
            }
            return m_ins;
        }
    }
}
