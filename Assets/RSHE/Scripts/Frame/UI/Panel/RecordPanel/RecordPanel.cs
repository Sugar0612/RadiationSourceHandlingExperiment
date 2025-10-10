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
    #region UI控件

    public PutItem PutItem_1;

    protected List<RecordItem> _recordList = new List<RecordItem>();

    protected int _itemIndex = 0;

    #endregion

    #region 检测参数
    public float ScanRadius = 1.5f; // 扫描半径

    //public float scanInterval = 1f; // 扫描间隔（秒）

    private float _timer;

    private List<Detector> _detctorList = new List<Detector>();

    public GameObject TestPoint;

    #endregion

    protected NetworkPropsCollider _propCollider;

    protected List<float> _valueList = new List<float>();

    virtual public void Start()
    {
        _itemIndex = 0;
        _recordList = GetComponentsInChildren<RecordItem>().ToList();
        _propCollider = GetComponentInParent<NetworkPropsCollider>();
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
