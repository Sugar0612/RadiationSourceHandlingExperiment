using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecordPanel : NetworkBehaviour
{
    #region UI¿Ø¼þ

    public PutItem PutItem_1;

    protected List<RecordItem> _recordList = new List<RecordItem>();

    protected int _itemIndex = 0;

    #endregion

    #region ¼ì²â²ÎÊý
    public float ScanRadius = 1.5f; // É¨Ãè°ë¾¶

    //public float scanInterval = 1f; // É¨Ãè¼ä¸ô£¨Ãë£©

    private float _timer;

    private List<Detector> _detctorList = new List<Detector>();

    public GameObject TestPoint;

    #endregion

    protected List<float> _valueList = new List<float>();

    virtual public void Start()
    {
        _itemIndex = 0;
        _recordList = GetComponentsInChildren<RecordItem>().ToList();
    }

    protected float ScanArea()
    {
        _detctorList.Clear();

        Collider[] hitColliders = Physics.OverlapSphere(TestPoint.transform.position, ScanRadius);

        foreach (Collider col in hitColliders)
        {
            Detector detector = col.gameObject.GetComponentInChildren<Detector>();
            if (detector)
            {
                _detctorList.Add(detector);
            }
        }

        float value = 0.0f;
        foreach (Detector detctor in _detctorList)
        {
            float temp = detctor.DeviceValue;
            value = Math.Max(value, temp);
        }
        return value;
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive<Image>(active);
        gameObject.SetActive<TextMeshProUGUI>(active);
    }
}
