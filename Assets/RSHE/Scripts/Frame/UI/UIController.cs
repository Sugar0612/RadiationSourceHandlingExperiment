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

    public List<WinBase> windows = new List<WinBase>();

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {

    }

    public void HidePanel()
    {
        gameObject.SetActive(false);
    }

    public void Register(WinBase window)
    {
        if (windows.Find(win => window.windowType == win.windowType))
        {
            //Log.cinput("red", "UIController: Register: " + window.name + " already registered");
            return;
        }
        //Log.cinput("green", $"UIController: Register type: {window.windowType}");
        windows.Add(window);
    }

    public void Unregister(WinBase window)
    {
        if (!windows.Find(win => window.windowType == win.windowType))
        {
            Log.cinput("red", "UIController: Unregister: " + window.name + " not registered");
            return;
        }

        windows.Remove(window);
    }

    public WinBase GetWindow<T>(EWindowType type) where T : WinBase
    {
        //Log.cinput("red", $"windows count: {windows.Count}, type: {type}");
        foreach (var window in windows)
        {
            if (window.GetType() == typeof(T) && window.windowType == type)
            {
                return window;
            }
        }

        // Log.cinput("red", "UIController: GetWindow: " + typeof(T).Name + " not found");
        return null;
    }

    public void ShowWindows(EWindowType types)
    {
        foreach (var win in windows)
        {
            bool shouldShow = (types & win.windowType) == win.windowType;
            win.SetActive(shouldShow);
            // Log.cinput("red", $"UIController: ShowWindows: {win.name} active: {shouldShow}");
        }
    }
}