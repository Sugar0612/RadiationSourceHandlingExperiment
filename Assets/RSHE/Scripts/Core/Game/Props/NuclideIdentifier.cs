using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class NuclideIdentifier: NetworkBehaviour
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

    [SyncVar]
    bool _isWork = false;

    public void Awake()
    {
        
    }

    public void Start()
    {
        OnWork.onClick.AddListener(() => _isWork = true);
        OffWork.onClick.AddListener(() => _isWork = false);
    }

    public void Update()
    {
        if (_isWork)
        {
            float val = Utility.Record(TestPoint);
            ValueText.text = val.ToString("F2") + "mSv/h";

            string Hint;
            if (NearVal <= val)
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
}
