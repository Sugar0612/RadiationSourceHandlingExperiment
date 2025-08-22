using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            Vector3 rsPos= new Vector3(rsTrans.position.x, 0.0f, rsTrans.position.z);
            Vector3 xzThisPos = new Vector3(go.transform.position.x, 0.0f, go.transform.position.z);

            disance = Vector3.Distance(rsPos, xzThisPos);
            //Log.cinput("yellow", $"@@@ disance_1：{disance_1}, disance_2: {disance_2}, ShowVal: {DistanceText.text}");
        }

        value = (rs.R * rs.A) / ((disance * disance));

        return value;
    }
}
