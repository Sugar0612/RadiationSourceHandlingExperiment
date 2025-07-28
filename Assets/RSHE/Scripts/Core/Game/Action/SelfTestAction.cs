using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> ×Ô²âÄ£Ê½ </summary>
public class SelfTestAction : BaseModeAction, IGameAction
{
    public void TaskOneStartAction() 
    {
        Log.cinput("yellow", "@@ SelfTestAction TaskOneStartAction..");
    }

    public void TaskOneAction(GameColliderPackage gamePkg)
    {
        Log.cinput("yellow", "@@ SelfTestAction TaskOneAction..");

        VRNetworkPlayerController ctrl = gamePkg?.VRPlayerCtrl.GetComponent<VRNetworkPlayerController>();
        if (!ctrl.isLocalPlayer)
        {
            ctrl.hat.SetRendererEnable(true);
            ctrl.clothes.SetRendererEnable(true);
        }
    }

    public void TaskOneEndAction() 
    {
        Log.cinput("yellow", "@@ SelfTestAction TaskOneEndAction..");
    }
}
