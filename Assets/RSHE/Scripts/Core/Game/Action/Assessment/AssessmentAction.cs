using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> ¿¼ºËÄ£Ê½ </summary>
public class AssessmentAction : BaseModeAction, IGameAction
{
    public void TaskOneStartAction(GameColliderPackage gamePkg)
    {
        Log.cinput("yellow", "@@ AssessmentAction TaskOneStartAction..");
    }

    public void TaskOneAction(GameColliderPackage gamePkg)
    {
        Log.cinput("yellow", "@@ AssessmentAction TaskOneAction..");
        
    }

    public void TaskOneEndAction(GameColliderPackage gamePkg) 
    {
        Log.cinput("yellow", "@@ AssessmentAction TaskOneEndAction..");
    }
}
