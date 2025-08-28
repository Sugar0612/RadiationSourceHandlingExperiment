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

    /// <summary> Task 3 start. </summary>
    public virtual void RpcStartAction_3(GameColliderPackage gamePkg) { }

    /// <summary> task 3 trigger collider. </summary>
    public virtual void RpcTaskAction_3(GameColliderPackage gamePkg) { }

    /// <summary> task 3 end. </summary>
    public virtual void RpcEndAction_3(GameColliderPackage gamePkg) { }

    public virtual void RpcStartAction_4(GameColliderPackage gamePkg) { }

    public virtual void RpcTaskAction_4(GameColliderPackage gamePkg) { }

    public virtual void RpcEndAction_4(GameColliderPackage gamePkg) { }

    public virtual void RpcStartAction_5(GameColliderPackage gamePkg) {  }

    public virtual void RpcTaskAction_5(GameColliderPackage gamePkg) { }

    public virtual void RpcEndAction_5(GameColliderPackage gamePkg) { }

    public virtual void RpcStartAction_6(GameColliderPackage gamePkg) {  }

    public virtual void RpcTaskAction_6(GameColliderPackage gamePkg) { }

    public virtual void RpcEndAction_6(GameColliderPackage gamePkg) { }

    public virtual void RpcStartAction_7(GameColliderPackage gamePkg) {  }

    public virtual void RpcTaskAction_7(GameColliderPackage gamePkg) { }

    public virtual void RpcEndAction_7(GameColliderPackage gamePkg) { }

    public virtual void RpcStartAction_8(GameColliderPackage gamePkg) {  }

    public virtual void RpcTaskAction_8(GameColliderPackage gamePkg) { }

    public virtual void RpcEndAction_8(GameColliderPackage gamePkg) { }

    public virtual void RpcStartAction_9(GameColliderPackage gamePkg) {  }

    public virtual void RpcTaskAction_9(GameColliderPackage gamePkg) { }

    public virtual void RpcEndAction_9(GameColliderPackage gamePkg) { }

    public virtual void RpcStartAction_10(GameColliderPackage gamePkg) {  }

    public virtual void RpcTaskAction_10(GameColliderPackage gamePkg) { }

    public virtual void RpcEndAction_10(GameColliderPackage gamePkg) { }

    public virtual void RpcStartAction_11(GameColliderPackage gamePkg) {  }

    public virtual void RpcTaskAction_11(GameColliderPackage gamePkg) { }

    public virtual void RpcEndAction_11(GameColliderPackage gamePkg) { }

    public virtual void RpcStartAction_12(GameColliderPackage gamePkg) {  }

    public virtual void RpcTaskAction_12(GameColliderPackage gamePkg) { }

    public virtual void RpcEndAction_12(GameColliderPackage gamePkg) { }

    public virtual void RpcStartAction_13(GameColliderPackage gamePkg) {  }

    public virtual void RpcTaskAction_13(GameColliderPackage gamePkg) { }

    public virtual void RpcEndAction_13(GameColliderPackage gamePkg) { }

    public virtual void RpcStartActionWait(GameColliderPackage gamePkg) { }

    public virtual void RpcTaskActionWait(GameColliderPackage gamePkg) { }

    public virtual void RpcEndActionWait(GameColliderPackage gamePkg) { }
}