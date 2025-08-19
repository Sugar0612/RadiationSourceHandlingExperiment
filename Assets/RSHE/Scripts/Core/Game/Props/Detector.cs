using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Detector : NetworkBehaviour
{
    /// <summary> 计算的最短距离显示 </summary>
    public TMP_Text DistanceText;

    public void Awake()
    {
        
    }

    public void Start()
    {
    }

    public void Update()
    {
        DistanceText.text = Utility.Record(gameObject).ToString("F2") + "mSv/h";
    }
}
