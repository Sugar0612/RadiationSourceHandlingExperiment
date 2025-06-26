using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using LitJson;
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
        StartCoroutine(InitializeConfig());
    }
}
