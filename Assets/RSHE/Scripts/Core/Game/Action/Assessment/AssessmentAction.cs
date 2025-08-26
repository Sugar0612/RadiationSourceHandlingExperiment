using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> ¿¼ºËÄ£Ê½ </summary>
public class AssessmentAction : ActionBase
{
    [ClientRpc]
    public override void RpcStartAction_1(GameColliderPackage gamePkg)
    {
        Log.cinput("yellow", "@@ AssessmentAction TaskOneStartAction..");
    }

    [ClientRpc]
    public override void RpcTaskAction_1(GameColliderPackage gamePkg)
    {
        Log.cinput("yellow", "@@ AssessmentAction TaskOneAction..");
    }


    [ClientRpc]
    public override void RpcEndAction_1(GameColliderPackage gamePkg) 
    {
        Log.cinput("yellow", "@@ AssessmentAction TaskOneEndAction..");
    }
}
