using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    static Game instance;

    public static Game Get()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<Game>();
        }

        return instance;
    }

    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
    }

    private void Start()
    {
        GameSteps.Get().ExecuteCurrentTask();
    }
}
