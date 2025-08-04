using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> ×Ô²âÄ£Ê½ </summary>
public class SelfTestAction : ActionBase
{
    [ClientRpc] public override void RpcStartAction_1(GameColliderPackage gamePkg)
    {
        Log.cinput("yellow", "@@ SelfTestAction TaskOneStartAction..");

        if (gamePkg != null)
        {
            AudioController.Get().Play(gamePkg.TaskItem.HintAudio);
        }
    }

    [ClientRpc] public override void RpcTaskAction_1(GameColliderPackage gamePkg)
    {
        Log.cinput("yellow", "@@ SelfTestAction TaskOneAction..");

        VRNetworkPlayerController ctrl = gamePkg?.VRPlayerCtrl.GetComponent<VRNetworkPlayerController>();
        if (ctrl)
        {
            // ctrl.hat.SetRendererEnable(true);
            // ctrl.clothes.SetRendererEnable(true);   

            if (gamePkg != null)
            {
                bool canGoOn = true;
                TaskCondition condition = gamePkg.TaskItem.conditions.Find(x => x.Identity == ctrl.identity);

                if (condition != null && condition.HoldingItemsIsEmpty())
                    condition.IsFinished = true;

                foreach (var item in gamePkg.TaskItem.conditions)
                    canGoOn = canGoOn & item.IsFinished;

                if (canGoOn)
                {
                    //if (StaticGlobalVar.IsHost)
                    //    gamePkg.TaskItem.GoEndTaskEvent();

                    // GameSteps.Get().Next();
                }
            }
        }
    }

    [ClientRpc] public override void RpcEndAction_1(GameColliderPackage gamePkg) 
    {
        Log.cinput("yellow", "@@ SelfTestAction TaskOneEndAction..");
    }


    /// <summary> Task 2 start. </summary>
    [ClientRpc] public override void RpcStartAction_2(GameColliderPackage gamePkg) { }

    [ClientRpc] public override void RpcTaskAction_2(GameColliderPackage gamePkg) 
    {
        if (gamePkg != null)
        {
            bool canGoOn = true;
            foreach (var condition in gamePkg.TaskItem.conditions)
            {
                bool isFinish = true;
                foreach (var item in condition.HoldingItems)
                {
                    isFinish = isFinish & (item.pCount == 0);
                }
                condition.IsFinished = isFinish;
                canGoOn = canGoOn & condition.IsFinished;
            }

            if (canGoOn)
            {
                if (StaticGlobalVar.IsHost)
                    gamePkg.TaskItem.GoEndTaskEvent();

                GameSteps.Get().Next();
            }
        }
    }

    /// <summary> task 2 end. </summary>
    [ClientRpc] public override void RpcEndAction_2(GameColliderPackage gamePkg) { }
}
