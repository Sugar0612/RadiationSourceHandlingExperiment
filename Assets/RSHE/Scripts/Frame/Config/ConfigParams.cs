using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
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
    public List<UserConfig> _userConfigList = new List<UserConfig>();

    private UserConfig _localUserConfig;

    bool _isConfigListInit = false;

    bool _isUserInit = false;

    public bool PicoDevice;

    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
    }

    public IEnumerator GetUserConfigList(Action<List<UserConfig>> callback)
    {
        if (_isConfigListInit)
        {
            callback(_userConfigList);
            yield break;
        }

        StartCoroutine(ConfigHelper.SetConfigObject(this, FilePath.UserConfigListPath, content =>
        {
            _userConfigList = JsonMapper.ToObject<List<UserConfig>>(content);
            _isConfigListInit = true;
            callback(_userConfigList);
        }));
    }

    public IEnumerator GetLocalIdentity(Action<EIdentity> callback)
    {
        if (_isUserInit)
        {
            callback(_localUserConfig.Identity);
            yield break;
        }

        StartCoroutine(ConfigHelper.SetConfigObject(this, FilePath.LocalUserIdentityPath, content =>
        {
            _localUserConfig = JsonMapper.ToObject<UserConfig>(content);
            _isUserInit = true;
            callback(_localUserConfig.Identity);
        }));
    }

}