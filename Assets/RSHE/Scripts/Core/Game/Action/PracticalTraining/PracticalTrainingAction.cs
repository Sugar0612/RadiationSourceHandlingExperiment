using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PracticalTrainingAction : ActionBase
{
    [ClientRpc]
    public override void RpcStartAction_1(GameColliderPackage gamePkg)
    {
        Log.cinput("yellow", "@@ PracticalTrainingAction TaskOneStartAction..");
    }

    [ClientRpc]
    public override void RpcTaskAction_1(GameColliderPackage gamePkg)
    {
        Log.cinput("yellow", "@@ PracticalTrainingAction TaskOneAction..");
    }

    [ClientRpc]
    public override void RpcEndAction_1(GameColliderPackage gamePkg)
    {
        Log.cinput("yellow", "@@ PracticalTrainingAction TaskOneEndAction..");
    }
}
