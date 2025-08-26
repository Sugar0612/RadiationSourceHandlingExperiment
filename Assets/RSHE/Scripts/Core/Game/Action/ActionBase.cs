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
    public virtual void StartAction_1(GameColliderPackage gamePkg) { }

    /// <summary> task one trigger collider. </summary>
    public virtual void TaskAction_1(GameColliderPackage gamePkg) { }

    /// <summary> task one end. </summary>
    public virtual void EndAction_1(GameColliderPackage gamePkg) { }

    /// <summary> Task 2 start. </summary>
    public virtual void StartAction_2(GameColliderPackage gamePkg) { }

    /// <summary> task 2 trigger collider. </summary>
    public virtual void TaskAction_2(GameColliderPackage gamePkg) { }

    /// <summary> task 2 end. </summary>
    public virtual void EndAction_2(GameColliderPackage gamePkg) { }

    /// <summary> Task 3 start. </summary>
    public virtual void StartAction_3(GameColliderPackage gamePkg) { }

    /// <summary> task 3 trigger collider. </summary>
    public virtual void TaskAction_3(GameColliderPackage gamePkg) { }

    /// <summary> task 3 end. </summary>
    public virtual void EndAction_3(GameColliderPackage gamePkg) { }
}