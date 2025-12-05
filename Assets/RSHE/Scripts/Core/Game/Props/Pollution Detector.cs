using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PollutionDetector : NetworkBehaviour
{
    public float ScanRadius = 1.5f; // 扫描半径

    public float scanInterval = 1f; // 扫描间隔（秒）

    private float _timer;

    private List<RadiationSource> _radiationSourceList = new List<RadiationSource>();

    /// <summary> 计算的最短距离显示 </summary>
    public TMP_Text DistanceText;

    /// <summary> 测试点 </summary>
    public GameObject TestPoint;

    public Collider TriggerCollider;

    private void Start()
    {

    }

    public void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= scanInterval)
        {
            _timer = 0;
            ScanArea();
        }

        // ScanArea();
    }

    void ScanArea()
    {
        _radiationSourceList.Clear();

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, ScanRadius);

        foreach (Collider col in hitColliders)
        {
            RadiationSource rs = col.gameObject.GetComponent<RadiationSource>();
            if (rs)
            {
                if (!rs.IsPickUpClear)
                {
                    _radiationSourceList.Add(rs);
                }
            }
        }

        float value = 0.0f;
        foreach (RadiationSource rs in _radiationSourceList)
        {
            float temp = Utility.Record(rs, TestPoint);
            value = Math.Max(value, temp / 1000.0f);
            if (value > 1.0f) value = 0.99f;
        }

        DistanceText.text = value.ToString("F2") + "mSv/h";
    }

    [ClientRpc]
    public void RpcInvalidateTargetPorpCollider()
    {
        TriggerCollider.enabled = false;
    }
}
