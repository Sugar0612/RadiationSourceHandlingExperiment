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

    /// <summary> 任务指向箭头 </summary>
    public Arrow Arrow;

    /// <summary>  </summary>
    public AudioClip HintAudio;

    /// <summary> 分数 </summary>
    public float fraction = 0.0f;

    /// <summary> 持续时间 </summary>
    public float duration = 0.0f;

    /// <summary> 任务完成条件 </summary>
    public List<TaskCondition> conditions = new List<TaskCondition>();

    /// <summary> 结束任务 </summary>
    public UnityEvent<GameColliderPackage> EndTask = null;
     
    /// <summary> 开始任务 </summary>
    public UnityEvent<GameColliderPackage> StartTask = null;

    /// <summary> 当玩家触发GameCollider后触发 </summary>
    public UnityEvent<GameColliderPackage> OnTask = null;

    /// <summary> 是否一直展示场景中该任务下的所有子物体 </summary>
    public bool IsAlwayShow = false;

    #endregion

    private void Awake()
    {
        SetActive(false);

        StartTask.AddListener(pkg => CmdStartActiveAction());
        EndTask.AddListener(pkg => CmdEndActiveAction());
    }

    #region 任务的开始与结束 [Base]
    /*
       这个region的作用就是让每个任务开始和结束相关的场景物品关闭，以及音频播放
       然后不同模式步骤不同的处理都放在了 Core/Game/Action 中
     */
    [Command(requiresAuthority = false)]
    void CmdStartActiveAction()
    {
        // Log.cinput("yellow", $"@@@ Start Action");
        RpcSetActive(true);
    }

    [Command(requiresAuthority = false)]
    void CmdEndActiveAction()
    {
        // Log.cinput("yellow", $"@@@ End Action");
        RpcSetActive(false);
    }

    #region 是否显示任务在 Network & Local
    [ClientRpc]
    void RpcSetActive(bool active)
    {
        // Log.cinput("yellow", $"@@@ RpcSetActive：{active}");

        gameObject.SetRendererEnable(active);
        gameObject.SetColliderEnable(active);
    }

    void SetActive(bool active)
    {
        gameObject.SetRendererEnable(active);
        gameObject.SetColliderEnable(active);
    }
    #endregion

    #endregion

    #region 控制 StartEvent 和 EndEvent的接口
    /// <summary> 执行 Task Start Event. </summary>
    public void GoStartTaskEvent()
    {
        GameColliderPackage pkg = new GameColliderPackage() { TaskItem = this };
        StartTask.Invoke(pkg);
    }

    /// <summary> 执行 Task End Event. </summary>
    public void GoEndTaskEvent()
    {
        GameColliderPackage pkg = new GameColliderPackage() { TaskItem = this };
        EndTask.Invoke(pkg);
    }
    #endregion
}
