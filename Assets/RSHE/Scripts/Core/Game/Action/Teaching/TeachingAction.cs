using Mirror;
using UnityEngine;

/// <summary> 教学模式 </summary>
public partial class TeachingAction : ActionBase
{
    [ClientRpc]
    public override void RpcStartAction_1(GameColliderPackage gamePkg)
    {
        if (gamePkg != null)
        {
            CoreAction.Get().SetTaskArrowActive(gamePkg, true);
            AudioController.Get().Play(gamePkg.TaskItem.HintAudio);
            Timer.Delay( gamePkg.TaskItem.duration,
                () => { CoreAction.Get().HostIssuesTheGoNext(gamePkg, true); } );
        }
    }

    [ClientRpc]
    public override void RpcTaskAction_1(GameColliderPackage gamePkg) { }

    [ClientRpc]
    public override void RpcEndAction_1(GameColliderPackage gamePkg) { }

    [ClientRpc]
    public override void RpcStartAction_2(GameColliderPackage gamePkg)
    {
        if (gamePkg != null)
        {
            CoreAction.Get().SetTaskArrowActive(gamePkg, true);
            AudioController.Get().Play(gamePkg.TaskItem.HintAudio);
            Timer.Delay(gamePkg.TaskItem.duration,
                () => { CoreAction.Get().HostIssuesTheGoNext(gamePkg, true); });
        }
    }

    [ClientRpc]
    public override void RpcTaskAction_2(GameColliderPackage gamePkg) { }

    [ClientRpc]
    public override void RpcEndAction_2(GameColliderPackage gamePkg) { }

    [ClientRpc]
    public override void RpcStartAction_3(GameColliderPackage gamePkg)
    {
        if (gamePkg != null)
        {
            CoreAction.Get().SetTaskArrowActive(gamePkg, true);
            AudioController.Get().Play(gamePkg.TaskItem.HintAudio);
            Timer.Delay(gamePkg.TaskItem.duration,
                () => { CoreAction.Get().HostIssuesTheGoNext(gamePkg, true); });
        }
    }

    [ClientRpc]
    public override void RpcTaskAction_3(GameColliderPackage gamePkg) { }

    [ClientRpc]
    public override void RpcEndAction_3(GameColliderPackage gamePkg) { }
}
