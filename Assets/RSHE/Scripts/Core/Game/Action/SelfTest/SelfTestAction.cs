using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 自测模式 </summary>
public class SelfTestAction : ActionBase
{
    [ClientRpc]
    public override void RpcStartAction_1(GameColliderPackage gamePkg)
    {
        // Log.cinput("red", "@@  SelfTestAction RpcStartAction_1");
        CoreAction.Get().SetTaskArrowActive(gamePkg, false);
        CoreAction.Get().StartAction_1(gamePkg);
    }

    [ClientRpc] public override void RpcTaskAction_1(GameColliderPackage gamePkg) { CoreAction.Get().TaskAction_1(gamePkg); }

    [ClientRpc] public override void RpcEndAction_1(GameColliderPackage gamePkg) { CoreAction.Get().EndAction_1(gamePkg); }

    [ClientRpc]
    public override void RpcStartAction_2(GameColliderPackage gamePkg) 
    {
        CoreAction.Get().SetTaskArrowActive(gamePkg, false);
        CoreAction.Get().StartAction_2(gamePkg); 
    }

    [ClientRpc]public override void RpcTaskAction_2(GameColliderPackage gamePkg) { CoreAction.Get().TaskAction_2(gamePkg); }

    [ClientRpc]public override void RpcEndAction_2(GameColliderPackage gamePkg) { CoreAction.Get().StartAction_2(gamePkg); }

    [ClientRpc]public override void RpcStartAction_3(GameColliderPackage gamePkg) { CoreAction.Get().StartAction_3(gamePkg); }

    [ClientRpc]public override void RpcTaskAction_3(GameColliderPackage gamePkg) { CoreAction.Get().TaskAction_3(gamePkg); }

    [ClientRpc] public override void RpcEndAction_3(GameColliderPackage gamePkg) { CoreAction.Get().StartAction_3(gamePkg); }
}
