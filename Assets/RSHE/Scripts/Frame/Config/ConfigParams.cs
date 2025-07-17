using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using LitJson;
using Mirror;
using Pico.Platform.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public partial class Config : MonoBehaviour
{
    [HideInInspector]
    public List<UserConfig> userConfig = new List<UserConfig>();

    public bool PicoDevice;

    private IEnumerator InitializeConfig()
    {
        var usrCfg = GetObject<List<UserConfig>>(result => userConfig = result, FilePath.UserConfigPath);
        yield return usrCfg;
    }

    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }

        StartCoroutine(InitializeConfig());
    }

    public EIdentity GetIdentityBaseOnDeviceID(string deviceID)
    {
        return userConfig.GetIdentityBaseOnDeviceID(deviceID);
    }
}


public static class ConfigExtensions
{
    public static EIdentity GetIdentityBaseOnDeviceID(this List<UserConfig> list, string deviceID)
    {
        UserConfig item = new UserConfig();

        item = list.Find(x => x.deviceID == deviceID);

        return item.identity;
    }
}