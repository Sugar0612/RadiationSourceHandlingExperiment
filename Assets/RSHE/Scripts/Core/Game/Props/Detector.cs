using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Detector : MonoBehaviour
{
    /// <summary> 放射源1 </summary>
    public Transform TargetTrans_1;

    /// <summary> 放射源2 </summary>
    public Transform TargetTrans_2;

    /// <summary> 计算的最短距离显示 </summary>
    public TMP_Text DistanceText;

    public void Awake()
    {
        
    }

    public void Start()
    {
        // _targetTrans_1 = GameObject.Find("放射源_1").gameObject.transform;
        // 
        // _targetTrans_1 = GameObject.Find("放射源_2").gameObject.transform;
    }

    public void Update()
    {
        // StartCoroutine(CalculateTheMinDistance());

        if (TargetTrans_1 != null && TargetTrans_2 != null)
        {
            float disance_1 = Vector3.Distance(gameObject.transform.position, TargetTrans_1.position);
            float disance_2 = Vector3.Distance(gameObject.transform.position, TargetTrans_2.position);
            DistanceText.text = Mathf.Min(disance_1, disance_2).ToString("F2") + "M";
            //Log.cinput("yellow", $"@@@ disance_1：{disance_1}, disance_2: {disance_2}, ShowVal: {DistanceText.text}");
        }
    }

    /// <summary> 计算显示最短放射源距离 </summary>
    IEnumerator CalculateTheMinDistance()
    {
        if (TargetTrans_1 != null && TargetTrans_2 != null)
        {
            float disance_1 = Vector3.Distance(gameObject.transform.position, TargetTrans_1.position);
            float disance_2 = Vector3.Distance(gameObject.transform.position, TargetTrans_2.position);
            DistanceText.text = Mathf.Min(disance_1, disance_2).ToString("F2") + "M";
        }

        yield return null;
    }
}
