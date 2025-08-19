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
    public static float Record(GameObject go)
    {
        float value = 0.0f;
        float disanceMin = 0;
        Transform targetTrans_1 = SceneObjectManager.Get().RadioactiveSource_1;
        Transform targetTrans_2 = SceneObjectManager.Get().RadioactiveSource_2;

        if (targetTrans_1 != null && targetTrans_2 != null)
        {
            Vector3 xzTargetPos_1 = new Vector3(targetTrans_1.position.x, 0.0f, targetTrans_1.position.z);
            Vector3 xzTargetPos_2 = new Vector3(targetTrans_2.position.x, 0.0f, targetTrans_2.position.z);
            Vector3 xzThisPos = new Vector3(go.transform.position.x, 0.0f, go.transform.position.z);

            float disance_1 = Vector3.Distance(xzThisPos, xzTargetPos_1);
            float disance_2 = Vector3.Distance(xzThisPos, xzTargetPos_2);
            disanceMin = Mathf.Min(disance_1, disance_2);
            //Log.cinput("yellow", $"@@@ disance_1：{disance_1}, disance_2: {disance_2}, ShowVal: {DistanceText.text}");
        }

        value = (0.332f * 1110f) / ((disanceMin * disanceMin) + 0.1f);

        return value;
    }
}
