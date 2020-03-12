using UnityEngine;
public abstract class ManagerSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T GetInstance()
    {
        if (_instance == null)
            _instance = FindObjectOfType<T>();
        return _instance;
    }
}