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

    [ClientRpc] public override void RpcTaskAction_4(GameColliderPackage gamePkg) { CoreAction.Get().TaskAction_4(gamePkg); }

    [ClientRpc] public override void RpcEndAction_4(GameColliderPackage gamePkg) { CoreAction.Get().EndAction_4(gamePkg); }

    [ClientRpc]
    public override void RpcStartAction_5(GameColliderPackage gamePkg) { CoreAction.Get().StartAction_5(gamePkg); }

    [ClientRpc]
    public override void RpcTaskAction_5(GameColliderPackage gamePkg)
    {
        RadiationSource[] radArray = FindObjectsOfType<RadiationSource>();
        foreach (RadiationSource rad in radArray)
        {
            if (rad.RName == "One" && rad.IsPickUpClear)
            {
                CoreAction.Get().TaskAction_5(gamePkg);
            }
        }
    }

    [ClientRpc] public override void RpcEndAction_5(GameColliderPackage gamePkg) { }

    [ClientRpc] public override void RpcStartActionWait(GameColliderPackage gamePkg) { CoreAction.Get().StartActionWait(gamePkg); }

    [ClientRpc] public override void RpcTaskActionWait(GameColliderPackage gamePkg) { CoreAction.Get().TaskActionWait(gamePkg); }

    [ClientRpc] public override void RpcEndActionWait(GameColliderPackage gamePkg) { CoreAction.Get().EndActionWait(gamePkg); }
}
