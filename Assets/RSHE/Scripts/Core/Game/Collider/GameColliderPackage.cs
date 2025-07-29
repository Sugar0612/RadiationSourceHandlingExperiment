using System;
using UnityEngine;

/// <summary>
/// 用来传递游戏重要内容
/// </summary>
[System.Serializable]
public class GameColliderPackage
{
    /// <summary> 触发这个Collider的玩家实例 </summary>
    public VRNetworkPlayerController VRPlayerCtrl;

    /// <summary> 任务信息 </summary>
    public GameTaskItem TaskItem;
}
