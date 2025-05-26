using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using LitJson;
using Unity.VisualScripting;
using UnityEngine;

public class Config : MonoBehaviour
{
    static Config instance;

    public static Config Get()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<Config>();
        }

        if (!_isInit)
            Init().Forget();

        return instance;
    }
    
    // 是否初始化完毕
    static bool _isInit = false;

    public bool isInit
    {
        get => _isInit;
    }

    /// <summary> 各种配置文件的接收对象 </summary>
    [HideInInspector]
    public ProjectConfig projectConfig = new ProjectConfig();

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// 初始化配置
    /// </summary>
    static async UniTaskVoid Init()
    {
        // 读取配置文件
        instance.projectConfig = await ConfigHelper.SetConfigObject<ProjectConfig>(FilePath.ProjectConfigPath);
        // Debug.Log("ProjectConfig: " + JsonMapper.ToJson(instance.projectConfig));

        // 设置初始化完毕
        _isInit = true;
    }
}
