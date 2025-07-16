using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class GameTaskItem : MonoBehaviour
{
    public string taskName = ""; // 任务名称
    public List<EIdentity> Executor = new List<EIdentity>(); // 执行人
    public UnityEvent task = null; // 执行方法
}
