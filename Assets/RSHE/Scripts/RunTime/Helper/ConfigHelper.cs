using System;
using System.Collections;
using LitJson;
using UnityEngine;

public class ConfigHelper
{
    public static IEnumerator SetConfigObject(MonoBehaviour runner, string filePath, Action<string> onLoaded = null)
    {
        string settingJson = null;
        yield return  runner.StartCoroutine(FileHelper.DownLoadTextFromServer(filePath, content => settingJson = content));

        if (string.IsNullOrEmpty(settingJson))
        {
            yield break;
        }

        //T config = JsonMapper.ToObject<T>(settingJson);
        onLoaded?.Invoke(settingJson);
    }
}