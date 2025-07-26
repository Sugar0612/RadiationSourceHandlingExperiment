using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PracticalTrainingAction : BaseModeAction, IGameAction
{
    public void TaskOneStartAction()
    {
        Log.cinput("yellow", "@@ PracticalTrainingAction TaskOneStartAction..");
    }

    public void TaskOneEndAction()
    {
        Log.cinput("yellow", "@@ PracticalTrainingAction TaskOneEndAction..");
    }
}
