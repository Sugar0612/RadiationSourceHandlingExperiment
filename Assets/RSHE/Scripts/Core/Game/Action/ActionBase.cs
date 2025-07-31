using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 不同游戏模式行为基类
/// </summary>
public abstract class ActionBase : NetworkBehaviour
{
    /// <summary> Task one start. </summary>
    public virtual void RpcStartAction_1(GameColliderPackage gamePkg) { }

    /// <summary> task one trigger collider. </summary>
    public virtual void RpcTaskAction_1(GameColliderPackage gamePkg) { }

    /// <summary> task one end. </summary>
    public virtual void RpcEndAction_1(GameColliderPackage gamePkg) { }
}