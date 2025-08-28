using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PracticalTrainingAction : ActionBase
{
    [ClientRpc]
    public override void RpcStartAction_1(GameColliderPackage gamePkg) 
    {
        CoreAction.Get().SetTaskArrowActive(gamePkg, true);
        AudioController.Get().Play(gamePkg.TaskItem.HintAudio);
        CoreAction.Get().StartAction_1(gamePkg); 
    }

    [ClientRpc]
    public override void RpcTaskAction_1(GameColliderPackage gamePkg) { CoreAction.Get().TaskAction_1(gamePkg); }

    [ClientRpc] public override void RpcEndAction_1(GameColliderPackage gamePkg) { CoreAction.Get().EndAction_1(gamePkg); }

    [ClientRpc]
    public override void RpcStartAction_2(GameColliderPackage gamePkg) 
    {
        CoreAction.Get().SetTaskArrowActive(gamePkg, true);
        AudioController.Get().Play(gamePkg.TaskItem.HintAudio);
        CoreAction.Get().StartAction_2(gamePkg); 
    }

    [ClientRpc] public override void RpcTaskAction_2(GameColliderPackage gamePkg) { CoreAction.Get().TaskAction_2(gamePkg); }

    [ClientRpc] public override void RpcEndAction_2(GameColliderPackage gamePkg) { CoreAction.Get().StartAction_2(gamePkg); }


    [ClientRpc]
    public override void RpcStartAction_3(GameColliderPackage gamePkg)
    {
        CoreAction.Get().SetTaskArrowActive(gamePkg, true);
        CoreAction.Get().StartAction_3(gamePkg);
    }

    [ClientRpc] public override void RpcTaskAction_3(GameColliderPackage gamePkg) { CoreAction.Get().TaskAction_2(gamePkg); }

    [ClientRpc] public override void RpcEndAction_3(GameColliderPackage gamePkg) { CoreAction.Get().StartAction_2(gamePkg); }



    [ClientRpc]
    public override void RpcStartActionWait(GameColliderPackage gamePkg)
    {
        CoreAction.Get().SetTaskArrowActive(gamePkg, true);
        CoreAction.Get().StartActionWait(gamePkg);
    }

    [ClientRpc] public override void RpcTaskActionWait(GameColliderPackage gamePkg) { CoreAction.Get().TaskActionWait(gamePkg); }

    [ClientRpc] public override void RpcEndActionWait(GameColliderPackage gamePkg) { CoreAction.Get().StartActionWait(gamePkg); }
}
