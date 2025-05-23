using System.Collections.Generic;
using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    static UIController instance;

    public static UIController Get()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<UIController>();
            if (instance == null)
            {
                GameObject obj = new GameObject("UIController");
                instance = obj.AddComponent<UIController>();
            }
        }
        return instance;
    }

    List<WinBase> windows = new List<WinBase>();

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        OnlyShowWindow(EWindowType.MainWindow);
    }

    public void HidePanel()
    {
        gameObject.SetActive(false);
    }


    public void Register(WinBase window)
    {
        if (windows.Contains(window))
        {
            Log.cinput("red", "UIController: Register: " + window.name + " already registered");
            return;
        }

        windows.Add(window);
    }

    public void Unregister(WinBase window)
    {
        if (!windows.Contains(window))
        {
            Log.cinput("red", "UIController: Unregister: " + window.name + " not registered");
            return;
        }

        windows.Remove(window);
    }

    public WinBase GetWindow<T>(EWindowType type) where T : WinBase
    {
        foreach (var window in windows)
        {
            if (window.GetType() == typeof(T) && window.windowType == type)
            {
                return window;
            }
        }

        Log.cinput("red", "UIController: GetWindow: " + typeof(T).Name + " not found");
        return null;
    }

    public void OnlyShowWindow(EWindowType type)
    {
        foreach (var window in windows)
        {
            if (window.windowType == type)
            {
                window.gameObject.SetActive(true);
            }
            else
            {
                window.gameObject.SetActive(false);
            }
        }
    }
}
