using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;

public class CameraManager : MonoBehaviour
{
    public List<CameraItem> cameraList = new List<CameraItem>();

    static CameraManager m_Instance;

    public static CameraManager Get()
    {
        if (m_Instance == null)
        {
            m_Instance = FindObjectOfType<CameraManager>();
            if (m_Instance == null)
            {
                GameObject obj = new GameObject("CameraManager");
                m_Instance = obj.AddComponent<CameraManager>();
            }
            DontDestroyOnLoad(m_Instance);
        }
        return m_Instance;
    }

    void Awake()
    {
        //if (instance != null)
        //{
        //    Destroy(gameObject);
        //    return;
        //}

        //instance = this;

        //DontDestroyOnLoad(gameObject);
    }

    public void Init()
    {
        // Log.cinput("yellow", "========= CameraManager Init");
        //foreach (var c in cameraList)
        //{
        //    Log.cinput("yellow", $"cameraList item tag: {c.cameraTag.ToString()}");
        //}

        if (!Config.Get().PicoDevice)
            SwitchCamera(CameraTag.Manager);
        else
            SwitchCamera(CameraTag.Player);
    }

    public void SwitchCamera(CameraTag tag)
    {
        foreach (CameraItem item in cameraList)
        {
            if (item == null)
                continue;

            if (tag == item.cameraTag)
            {
                item.gameObject.SetActive(true);
                item.tag = "MainCamera";
            }
            else
            {
                item.tag = "Untagged";
                item.gameObject.SetActive(false);
            }
        }
    }

    public void SwitchCamera(string s_tag)
    {
        foreach (CameraItem item in cameraList)
        {
            CameraTag tag = CameraTag.None;

            if (item == null || !Enum.TryParse(s_tag, out tag)) continue;

            if (tag == item.cameraTag)
                item.tag = "MainCamera";
            else
                item.tag = "Untagged";
        }
    }

    public void Register(CameraItem item)
    {
        if (!cameraList.Find(it => it.cameraTag == item.cameraTag))
        {
            cameraList.Add(item);
            // item.tag = "Untagged";
        }
    }

    public void Remove(CameraItem item)
    {
        if (cameraList.Find(it => it.cameraTag == item.cameraTag))
        {
            item.tag = "Untagged";
            cameraList.Remove(item);
        }
    }

    public static bool InstanceIsNull() => m_Instance == null;
}