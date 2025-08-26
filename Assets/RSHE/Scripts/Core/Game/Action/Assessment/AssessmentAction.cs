using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> ¿¼ºËÄ£Ê½ </summary>
public class AssessmentAction : ActionBase
{
    public override void StartAction_1(GameColliderPackage gamePkg)
    {
        Log.cinput("yellow", "@@ AssessmentAction TaskOneStartAction..");
    }

    
    public override void TaskAction_1(GameColliderPackage gamePkg)
    {
        Log.cinput("yellow", "@@ AssessmentAction TaskOneAction..");
    }

    
    public override void EndAction_1(GameColliderPackage gamePkg) 
    {
        Log.cinput("yellow", "@@ AssessmentAction TaskOneEndAction..");
    }
}
