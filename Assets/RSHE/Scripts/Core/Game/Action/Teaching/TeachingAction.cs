using Mirror;
using UnityEngine;

/// <summary> 教学模式 </summary>
public partial class TeachingAction : ActionBase
{
    public override void StartAction_1(GameColliderPackage gamePkg)
    {
        if (gamePkg != null)
        {
            CoreAction.Get().SetTaskArrowActive(gamePkg, true);
            AudioController.Get().Play(gamePkg.TaskItem.HintAudio);
            Timer.Delay(gamePkg.TaskItem.duration,
                () => { CoreAction.Get()?.HostIssuesTheGoNext(gamePkg, true); });
        }
    }

    public override void TaskAction_1(GameColliderPackage gamePkg) { }

    public override void EndAction_1(GameColliderPackage gamePkg) { }

    public override void StartAction_2(GameColliderPackage gamePkg)
    {
        if (gamePkg != null)
        {
            CoreAction.Get().SetTaskArrowActive(gamePkg, true);
            AudioController.Get().Play(gamePkg.TaskItem.HintAudio);
            Timer.Delay(gamePkg.TaskItem.duration,
                () => { CoreAction.Get()?.HostIssuesTheGoNext(gamePkg, true); });
        }
    }

    public override void TaskAction_2(GameColliderPackage gamePkg) { }

    public override void EndAction_2(GameColliderPackage gamePkg) { }

    public override void StartAction_3(GameColliderPackage gamePkg)
    {
        if (gamePkg != null)
        {
            CoreAction.Get().SetTaskArrowActive(gamePkg, true);
            AudioController.Get().Play(gamePkg.TaskItem.HintAudio);
            Timer.Delay(gamePkg.TaskItem.duration,
                () => { CoreAction.Get().HostIssuesTheGoNext(gamePkg, true); });
        }
    }

    public override void TaskAction_3(GameColliderPackage gamePkg) { }

    public override void EndAction_3(GameColliderPackage gamePkg) { }
}
