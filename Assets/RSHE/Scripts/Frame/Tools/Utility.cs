using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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


    /// <summary> 图片载入 </summary>
    public static void LoadImageFromResource(Image img, string imgPath)
    {
        Sprite newSprite = Resources.Load<Sprite>(imgPath);
        if (newSprite != null)
        {
            img.sprite = newSprite;
        }
        else
        {
            // Debug.LogError($"Failed to load image from Resources: {imgPath}");
        }
    }
}
