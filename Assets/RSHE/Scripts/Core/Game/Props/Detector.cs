using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Detector : NetworkBehaviour
{
    public float ScanRadius = 1.5f; // …®√Ë∞Îæ∂

    public float scanInterval = 1f; // …®√Ëº‰∏Ù£®√Î£©

    private float _timer;

    private List<RadiationSource> _radiationSourceList = new List<RadiationSource>();

    /// <summary> º∆À„µƒ◊Ó∂Ãæ‡¿Îœ‘ æ </summary>
    public TMP_Text DistanceText;

    /// <summary> ≤‚ ‘µ„ </summary>
    public GameObject TestPoint;

    public Collider TriggerCollider;

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
            value = Math.Max(value, Utility.Record(rs, TestPoint));
            value = value / 1000.0f;
        }

        DistanceText.text = value.ToString("F2") + "mSv/h";
    }

    [ClientRpc]
    public void RpcInvalidateTargetPorpCollider()
    {
        if (TriggerCollider != null)
            TriggerCollider.enabled = false;
    }
}
