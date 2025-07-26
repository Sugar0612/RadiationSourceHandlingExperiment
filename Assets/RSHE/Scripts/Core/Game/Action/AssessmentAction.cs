using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> ¿¼ºËÄ£Ê½ </summary>
public class AssessmentAction : BaseModeAction, IGameAction
{
    public void TaskOneStartAction()
    {
        Log.cinput("yellow", "@@ AssessmentAction TaskOneStartAction..");
    }

    public void TaskOneEndAction() 
    {
        Log.cinput("yellow", "@@ AssessmentAction TaskOneEndAction..");
    }
}
