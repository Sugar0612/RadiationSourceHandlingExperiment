using System.Collections.Generic;
using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    static UIController m_Instance;

    public static UIController Get()
    {
        if (m_Instance == null)
        {
            m_Instance = FindObjectOfType<UIController>();
            if (m_Instance == null)
            {
                GameObject obj = new GameObject("UIController");
                m_Instance = obj.AddComponent<UIController>();
            }
        }
        return m_Instance;
    }

    public List<WinBase> windows = new List<WinBase>();

    void Awake()
    {
        if (m_Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        m_Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {

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

    public void HideWindows(EWindowType types)
    {
        foreach (var win in windows)
        {
            bool isTarget = (types & win.windowType) == win.windowType;
            if (isTarget)
                win.SetActive(false);
        }
    }
}