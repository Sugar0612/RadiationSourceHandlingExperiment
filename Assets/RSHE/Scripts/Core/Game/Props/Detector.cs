using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Detector : NetworkBehaviour
{
    /// <summary> 放射源1 </summary>
    Transform _targetTrans_1;

    /// <summary> 放射源2 </summary>
    Transform _targetTrans_2;

    /// <summary> 计算的最短距离显示 </summary>
    public TMP_Text DistanceText;

    public void Awake()
    {
        
    }

    public void Start()
    {
        _targetTrans_1 = SceneObjectManager.Get().RadioactiveSource_1;

        _targetTrans_2 = SceneObjectManager.Get().RadioactiveSource_2;
    }

    public void Update()
    {
        // StartCoroutine(CalculateTheMinDistance());

        if (_targetTrans_1 != null && _targetTrans_2 != null)
        {
            Vector3 xzTargetPos_1 = new Vector3(_targetTrans_1.position.x, 0.0f, _targetTrans_1.position.z);
            Vector3 xzTargetPos_2 = new Vector3(_targetTrans_2.position.x, 0.0f, _targetTrans_2.position.z);
            Vector3 xzThisPos = new Vector3(gameObject.transform.position.x, 0.0f, gameObject.transform.position.z);

            float disance_1 = Vector3.Distance(xzThisPos, xzTargetPos_1);
            float disance_2 = Vector3.Distance(xzThisPos, xzTargetPos_2);
            DistanceText.text = Mathf.Min(disance_1, disance_2).ToString("F2") + "M";
            //Log.cinput("yellow", $"@@@ disance_1：{disance_1}, disance_2: {disance_2}, ShowVal: {DistanceText.text}");
        }
    }

    /// <summary> 计算显示最短放射源距离 </summary>
    IEnumerator CalculateTheMinDistance()
    {
        if (_targetTrans_1 != null && _targetTrans_2 != null)
        {
            float disance_1 = Vector3.Distance(gameObject.transform.position, _targetTrans_1.position);
            float disance_2 = Vector3.Distance(gameObject.transform.position, _targetTrans_2.position);
            DistanceText.text = Mathf.Min(disance_1, disance_2).ToString("F2") + "M";
        }

        yield return null;
    }
}
