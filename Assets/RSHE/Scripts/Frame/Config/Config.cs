using System;
using System.Collections;
using LitJson;
using UnityEngine;

public partial class Config : MonoBehaviour
{
    static Config instance;

    public static Config Get()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<Config>();
        }

        return instance;
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private IEnumerator GetObject<T>(Action<T> callback, string filePath)
    {
        string jsonContent = null;

        yield return StartCoroutine(ConfigHelper.SetConfigObject(this, filePath, content => jsonContent = content));

        T result = JsonMapper.ToObject<T>(jsonContent);

        callback?.Invoke(result);
    }
}
