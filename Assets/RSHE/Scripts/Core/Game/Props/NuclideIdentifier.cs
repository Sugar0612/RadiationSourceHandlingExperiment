using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class NuclideIdentifier: PropBase
{
    /// <summary> 计算的最短距离显示 </summary>
    public TMP_Text ValueText;

    /// <summary> 未检测到核素 or 检测到核素 /// </summary>
    public TMP_Text HintText;

    /// <summary> 开始工作 </summary>
    public Button OnWork;

    /// <summary> 结束工作 </summary>
    public Button OffWork;

    /// <summary> 检测点 </summary>
    public GameObject TestPoint;

    /// <summary> 近点值 </summary>
    public float NearVal;

    #region 检测参数
    public float ScanRadius = 4.0f; // 扫描半径

    public float scanInterval = 1f; // 扫描间隔（秒）

    private float _timer;

    private List<RadiationSource> _radiationSourceList = new List<RadiationSource>();
    #endregion

    [SyncVar]
    bool _isWork = false;

    public void Awake()
    {
        
    }

    public void Start()
    {
        OnWork.onClick.AddListener(() => CmdSetIsWork(true));
        OffWork.onClick.AddListener(() => CmdSetIsWork(false));
    }

    public void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= scanInterval)
        {
            _timer = 0;
            ScanArea();
        }
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

        UpdateView(value);
    }

    void UpdateView(float value)
    {
        if (_isWork)
        {
            ValueText.text = value.ToString("F2") + "mSv/h";

            string Hint;
            if (NearVal <= value)
            {
                Hint = @"检测到: 137Cs";
                HintText.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
                HintText.text = Hint;
            }
            else
            {
                Hint = @"未检测到核素";
                HintText.GetComponent<TextMeshProUGUI>().color = new Color(0.0f, 1.0f, 0.0f, 1.0f);
                HintText.text = Hint;
            }
        }
    }

    [Command]
    void CmdSetIsWork(bool isWork)
    {
        _isWork = isWork;
    }
}
