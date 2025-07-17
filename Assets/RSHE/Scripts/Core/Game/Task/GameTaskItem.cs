using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using System;

[Serializable]
public class GameTaskItem : MonoBehaviour
{
    public string taskName = ""; // 任务名称
    public List<EIdentity> executorsList = new List<EIdentity>(); // 执行人
    public UnityEvent task = null; // 执行方法
}
