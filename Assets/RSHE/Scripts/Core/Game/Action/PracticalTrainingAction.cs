using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PracticalTrainingAction : BaseModeAction, IGameAction
{
    public void TaskOneStartAction()
    {
        Log.cinput("yellow", "@@ PracticalTrainingAction TaskOneStartAction..");
    }

    public void TaskOneAction(GameColliderPackage gamePkg)
    {
        Log.cinput("yellow", "@@ PracticalTrainingAction TaskOneAction..");
    }

    public void TaskOneEndAction()
    {
        Log.cinput("yellow", "@@ PracticalTrainingAction TaskOneEndAction..");
    }
}
