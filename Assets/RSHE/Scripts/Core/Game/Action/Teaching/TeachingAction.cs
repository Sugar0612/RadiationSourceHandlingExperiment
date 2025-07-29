

using UnityEngine;

/// <summary> 教学模式 </summary>
public partial class TeachingAction : BaseModeAction, IGameAction
{
    public void TaskOneStartAction(GameColliderPackage gamePkg)
    {
        Log.cinput("yellow", "@@@  TeachingAction TaskOneStartAction");

        if (gamePkg != null)
        {
            AudioController.Get().Play(gamePkg.TaskItem.HintAudio);
            Timer.Delay(gamePkg.TaskItem.duration, () =>
            {
            Log.cinput("yellow", "Go on next task base one task.");
                gamePkg.TaskItem.GoEndTaskEvent();
                GameSteps.Get().Next();
            });
        }

    }

    public void TaskOneAction(GameColliderPackage gamePkg)
    {
        Log.cinput("yellow", "@@ TeachingAction TaskOneAction..");
    }

    public void TaskOneEndAction(GameColliderPackage gamePkg) 
    {
        Log.cinput("yellow", "@@ TeachingAction TaskOneEndAction..");
    }
}
