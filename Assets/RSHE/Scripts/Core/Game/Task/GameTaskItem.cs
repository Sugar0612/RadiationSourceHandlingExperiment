using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.Rendering;
using Mirror;
using Unity.VisualScripting;

[Serializable]
public class GameTaskItem : NetworkBehaviour
{
    #region 需要编辑变量
    public string taskName = ""; // 任务名称
    // public List<TaskCondition> executorsList = new List<TaskCondition>(); // 执行人
    #endregion

    Renderer[] renderList;
    Collider[] colliderList;
    [HideInInspector] public UnityEvent StartTask = null; // 开始任务
    [HideInInspector] public UnityEvent EndTask = null; // 结束任务

    private void Awake()
    {
        renderList = GetComponentsInChildren<Renderer>();
        colliderList = GetComponentsInChildren<Collider>();

        SetActive(false);

        StartTask.AddListener(() => CmdStartAction());
        EndTask.AddListener(() => CmdEndAction());
    }

    [Command(requiresAuthority = false)]
    void CmdStartAction()
    {
        RpcSetActive(true);
    }

    [Command(requiresAuthority = false)]
    void CmdEndAction()
    {
        Log.cinput("yellow", $"@@@ EndAction");
        RpcSetActive(false);
    }

    [ClientRpc]
    void RpcSetActive(bool active)
    {
        Log.cinput("yellow", $"@@@ RpcSetActive：{active}");
        for (int i = 0; i < renderList.Count(); ++i)
        {
            renderList[i].enabled = active;
        }

        for (int i = 0; i < colliderList.Count(); ++i)
        {
            colliderList[i].enabled = active;
        }
    }

    void SetActive(bool active)
    {
        for (int i = 0; i < renderList.Count(); ++i)
        {
            renderList[i].enabled = active;
        }

        for (int i = 0; i < colliderList.Count(); ++i)
        {
            colliderList[i].enabled = active;
        }
    }
}
