using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    static T instance;
    private void Awake()
    {
        CreateSingleton();
    }

    void CreateSingleton()
    {
        T found = (T)FindObjectOfType(typeof(T));
        if (instance != null && instance != found)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = found;
        }

        DontDestroyOnLoad(gameObject);
    }
}
