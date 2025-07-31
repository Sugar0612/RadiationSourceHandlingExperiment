using Mirror;
using UnityEngine;

/// <summary> 教学模式 </summary>
public partial class TeachingAction : ActionBase
{
    [ClientRpc]
    public override void RpcStartAction_1(GameColliderPackage gamePkg)
    {
        Log.cinput("yellow", "@@@  TeachingAction TaskOneStartAction");

        if (gamePkg != null)
        {
            AudioController.Get().Play(gamePkg.TaskItem.HintAudio);
            Timer.Delay(
                gamePkg.TaskItem.duration,
                () =>
                {
                    if (StaticGlobalVar.IsHost)
                    {
                        gamePkg.TaskItem.GoEndTaskEvent();
                    }

                    GameSteps.Get().Next();
                }
            );
        }
    }

    [ClientRpc]
    public override void RpcTaskAction_1(GameColliderPackage gamePkg)
    {
        Log.cinput("yellow", "@@ TeachingAction TaskOneAction..");
    }

    [ClientRpc]
    public override void RpcEndAction_1(GameColliderPackage gamePkg)
    {
        Log.cinput("yellow", "@@ TeachingAction TaskOneEndAction..");
    }
}
