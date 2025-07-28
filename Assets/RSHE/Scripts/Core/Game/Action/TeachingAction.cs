using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 教学模式 </summary>
public class TeachingAction : BaseModeAction, IGameAction
{
    public void TaskOneStartAction()
    {
        Log.cinput("yellow", "@@ TeachingAction TaskOneStartAction..");
    }

    public void TaskOneAction(GameColliderPackage gamePkg)
    {
        Log.cinput("yellow", "@@ TeachingAction TaskOneAction..");
    }

    public void TaskOneEndAction() 
    {
        Log.cinput("yellow", "@@ TeachingAction TaskOneEndAction..");
    }
}
