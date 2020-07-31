using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private T m_Instance;

    public T Instance
    {
        get
        {
            T instance = FindObjectOfType<T>();
            if (m_Instance == null)
            {
                m_Instance = instance;
            }else if (m_Instance != instance)
            {
                Destroy(instance);
            }

            return m_Instance;
        }
    }
}
