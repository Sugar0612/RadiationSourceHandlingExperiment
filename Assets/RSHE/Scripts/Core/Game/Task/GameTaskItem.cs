using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.Rendering;
using Mirror;
using Unity.VisualScripting;
using UnityEngine.InputSystem;

[Serializable]
public class GameTaskItem : NetworkBehaviour
{
    #region 游戏参数 & 触发模式

    /// <summary> 任务名称 </summary>
    public string taskName = "";

    /// <summary> 分数 </summary>
    public float fraction = 0.0f;

    /// <summary> 任务完成条件 </summary>
    public List<TaskCondition> conditions = new List<TaskCondition>();

    /// <summary> 当玩家触发GameCollider后触发 </summary>
    public UnityEvent<GameColliderPackage> OnTask = null;

    /// <summary> 结束任务 </summary>
    public UnityEvent EndTask = null;
     
    /// <summary> 开始任务 </summary>
    public UnityEvent StartTask = null;

    #endregion

    private void Awake()
    {
        SetActive(false);

        StartTask.AddListener(() => CmdStartAction());
        EndTask.AddListener(() => CmdEndAction());
    }

    #region 任务的开始与结束
    [Command(requiresAuthority = false)]
    void CmdStartAction()
    {
        Log.cinput("yellow", $"@@@ Start Action");
        RpcSetActive(true);
    }

    [Command(requiresAuthority = false)]
    void CmdEndAction()
    {
        Log.cinput("yellow", $"@@@ End Action");
        RpcSetActive(false);
    }
    #endregion

    #region 是否显示任务在 Network & Local
    [ClientRpc]
    void RpcSetActive(bool active)
    {
        Log.cinput("yellow", $"@@@ RpcSetActive：{active}");

        gameObject.SetRendererEnable(active);
        gameObject.SetColliderEnable(active);
    }

    void SetActive(bool active)
    {
        gameObject.SetRendererEnable(active);
        gameObject.SetColliderEnable(active);
    }
    #endregion
}
