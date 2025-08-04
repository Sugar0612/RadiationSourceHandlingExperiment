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

    /// <summary> Task 2 start. </summary>
    public virtual void RpcStartAction_2(GameColliderPackage gamePkg) { }

    /// <summary> task 2 trigger collider. </summary>
    public virtual void RpcTaskAction_2(GameColliderPackage gamePkg) { }

    /// <summary> task 2 end. </summary>
    public virtual void RpcEndAction_2(GameColliderPackage gamePkg) { }
}