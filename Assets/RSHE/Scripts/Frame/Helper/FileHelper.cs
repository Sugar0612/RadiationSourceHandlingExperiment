using System.IO;
using UnityEngine.Networking;
using Cysharp.Threading.Tasks;
using System;
using System.Threading.Tasks;

public class FileHelper
{
    /// <summary>
    /// 根据 path_url 找到文本内容
    /// </summary>
    /// <param name="path_url"></param>
    /// <param name="callback"></param>
    /// <returns></returns>
    // public static async UniTask ReadFileAsync(string path_url, Action<string> callback = null)
    // {
    //     string content = await DownLoadTextFromServer(path_url);
    //     callback?.Invoke(content);
    // }


    public static async UniTask<string> DownLoadTextFromServer(string path_url)
    {
        UnityWebRequest req = UnityWebRequest.Get(path_url);
        await req.SendWebRequest();

        string content = req.downloadHandler.text;

        return content;
    }

    public static void WriteFile(string filePath, string content)
    {
        if (!File.Exists(filePath))
        {
            File.Create(filePath).Close();
        }

        File.WriteAllTextAsync(filePath, content);
    }
}