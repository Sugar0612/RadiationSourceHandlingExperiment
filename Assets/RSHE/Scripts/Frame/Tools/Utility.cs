using Mirror;
using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public static class Utility
{
    /// <summary>
    /// 计算两个辐射点钟最近的辐射点强度
    /// </summary>
    /// <param name="posX"></param>
    /// <param name="posZ"></param>
    /// <returns></returns>
    public static float Record(RadiationSource rs, GameObject go)
    {
        float value = 0.0f;
        float disance = 0;
        Transform rsTrans = rs.gameObject.transform;

        if (rsTrans != null)
        {
            Vector3 rsPos = new Vector3(rsTrans.position.x, 0.0f, rsTrans.position.z);
            Vector3 xzThisPos = new Vector3(go.transform.position.x, 0.0f, go.transform.position.z);

            disance = Vector3.Distance(rsPos, xzThisPos);
            //Log.cinput("yellow", $"@@@ disance_1：{disance_1}, disance_2: {disance_2}, ShowVal: {DistanceText.text}");
        }

        value = (rs.R * rs.A) / ((disance * disance));

        return value;
    }

    /// <summary> 销毁网络Object </summary>

    [Server]
    public static void DestroyNetworkObject(GameObject targetObject)
    {
        Log.cinput("red", "@@@ DestroyNetworkObject");
        if (targetObject == null) return;

        NetworkIdentity identity = targetObject.GetComponentInChildren<NetworkIdentity>();
        if (identity == null)
            identity = targetObject.GetComponentInParent<NetworkIdentity>();

        if (NetworkServer.active && identity)
        {
            Log.cinput("red", "@@@ In DestroyNetworkObject");
            NetworkServer.Destroy(identity.gameObject);
        }
    }


    /// <summary> 图片载入(从 Addressables 按地址异步加载) </summary>
    public static void LoadImageFromAddressables(Image img, string address)
    {
        Addressables.LoadAssetAsync<Sprite>(address).Completed += op =>
        {
            if (op.Status == AsyncOperationStatus.Succeeded)
            {
                img.sprite = op.Result;
            }
            else
            {
                Log.cinput("red", $"[Addressables] 背景图加载失败: {address}, {op.OperationException}");
            }
        };
    }

    public static string GetLocalTime()
    {
        return DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
    }

    public static bool OverwriteJSONFile(string filePath, string newContent)
    {
        if (!File.Exists(filePath))
        {
            Debug.LogWarning("文件不存在，将创建新文件: " + filePath);
        }

        try
        {
            // 将新内容写入文件，完全覆盖原有内容
            File.WriteAllText(filePath, newContent);
            Debug.Log("JSON文件覆盖成功: " + filePath);
            return true;
        }
        catch (System.Exception e)
        {
            Debug.LogError("写入文件时出错: " + e.Message);
        }
        return false;
    }

    public static IEnumerator ReadFile(string filePath, System.Action<string> callback)
    {
        if (filePath.StartsWith("http"))
        {
            using (UnityWebRequest request = UnityWebRequest.Get(filePath))
            {
                yield return request.SendWebRequest();

                if (request.result == UnityWebRequest.Result.Success)
                    callback(request.downloadHandler.text);
                else
                    callback(null);
            }
        }
        else
        {
            string localStr = "";
            if (File.Exists(filePath))
            {
                Log.cinput("red", "Json文件读取成功");
                localStr = File.ReadAllText(filePath);
            }
            callback(localStr);
        }
    }
}
