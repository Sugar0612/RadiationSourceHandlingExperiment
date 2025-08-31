using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PracticalTrainingAction : ActionBase
{
    [ClientRpc]
    public override void RpcStartAction_1(GameColliderPackage gamePkg) 
    {
        CoreAction.Get().SetTaskArrowActive(gamePkg, true);
        AudioController.Get().Play(gamePkg.TaskItem.HintAudio);
        CoreAction.Get().StartAction_1(gamePkg); 
    }

    [ClientRpc]
    public override void RpcTaskAction_1(GameColliderPackage gamePkg) 
    {
        if (!GameSteps.Get().IsCheckTaskFinished(TaskName.T1))
        {
            CoreAction.Get().TaskAction_1(gamePkg);
        }
    }

    [ClientRpc] public override void RpcEndAction_1(GameColliderPackage gamePkg) { CoreAction.Get().EndAction_1(gamePkg); }

    [ClientRpc]
    public override void RpcStartAction_2(GameColliderPackage gamePkg) 
    {
        CoreAction.Get().SetTaskArrowActive(gamePkg, true);
        AudioController.Get().Play(gamePkg.TaskItem.HintAudio);
        CoreAction.Get().StartAction_2(gamePkg); 
    }

    [ClientRpc] public override void RpcTaskAction_2(GameColliderPackage gamePkg) { CoreAction.Get().TaskAction_2(gamePkg); }

    [ClientRpc] public override void RpcEndAction_2(GameColliderPackage gamePkg) { CoreAction.Get().EndAction_2(gamePkg); }


    [ClientRpc]
    public override void RpcStartAction_3(GameColliderPackage gamePkg)
    {
        Log.cinput("yellow", "RpcStartAction_3");
        CoreAction.Get().SetTaskArrowActive(gamePkg, true);
        AudioController.Get().Play(gamePkg.TaskItem.HintAudio);
        CoreAction.Get().StartAction_3(gamePkg);
    }

    [ClientRpc] public override void RpcTaskAction_3(GameColliderPackage gamePkg) { CoreAction.Get().TaskAction_3(gamePkg); }

    [ClientRpc] public override void RpcEndAction_3(GameColliderPackage gamePkg) { CoreAction.Get().StartAction_3(gamePkg); }

    [ClientRpc]
    public override void RpcStartAction_4(GameColliderPackage gamePkg)
    {
        Log.cinput("yellow", "RpcStartAction_4");
        CoreAction.Get().SetTaskArrowActive(gamePkg, true);
        AudioController.Get().Play(gamePkg.TaskItem.HintAudio);
        CoreAction.Get().StartAction_4(gamePkg);
    }

    [ClientRpc] public override void RpcTaskAction_4(GameColliderPackage gamePkg) { Log.cinput("yellow", "RpcTaskAction_4"); CoreAction.Get().TaskAction_4(gamePkg); }

    [ClientRpc] public override void RpcEndAction_4(GameColliderPackage gamePkg) { CoreAction.Get().EndAction_4(gamePkg); }

    [ClientRpc] public override void RpcStartAction_5(GameColliderPackage gamePkg)
    {
        CoreAction.Get().SetTaskArrowActive(gamePkg, true);
        AudioController.Get().Play(gamePkg.TaskItem.HintAudio);
        CoreAction.Get().StartAction_5(gamePkg);
    }

    [ClientRpc] public override void RpcTaskAction_5(GameColliderPackage gamePkg) 
    {
        Log.cinput("yellow", "RpcStartAction_5");
        RadiationSource[] radArray = FindObjectsOfType<RadiationSource>();
        foreach (RadiationSource rad in radArray)
        {
            if (rad.RName == "One" && rad.IsPickUpClear)
            {
                CoreAction.Get().TaskAction_5(gamePkg);
            }
        }
    }

    [ClientRpc] public override void RpcEndAction_5(GameColliderPackage gamePkg) { }

    [ClientRpc] public override void RpcStartAction_6(GameColliderPackage gamePkg)
    {
        CoreAction.Get().SetTaskArrowActive(gamePkg, true);
        AudioController.Get().Play(gamePkg.TaskItem.HintAudio);
        CoreAction.Get().StartAction_6(gamePkg);
    }

    [ClientRpc] public override void RpcTaskAction_6(GameColliderPackage gamePkg) { CoreAction.Get().TaskAction_6(gamePkg); }

    [ClientRpc] public override void RpcEndAction_6(GameColliderPackage gamePkg) { }

    [ClientRpc]
    public override void RpcStartAction_7(GameColliderPackage gamePkg)
    {
        CoreAction.Get().SetTaskArrowActive(gamePkg, true);
        AudioController.Get().Play(gamePkg.TaskItem.HintAudio);
        CoreAction.Get().StartAction_7(gamePkg);
    }

    [ClientRpc] public override void RpcTaskAction_7(GameColliderPackage gamePkg) { CoreAction.Get().TaskAction_7(gamePkg); }

    [ClientRpc] public override void RpcEndAction_7(GameColliderPackage gamePkg) { }

    [ClientRpc]
    public override void RpcStartAction_8(GameColliderPackage gamePkg)
    {
        CoreAction.Get().SetTaskArrowActive(gamePkg, true);
        AudioController.Get().Play(gamePkg.TaskItem.HintAudio);
        CoreAction.Get().StartAction_8(gamePkg);
    }

    [ClientRpc] public override void RpcTaskAction_8(GameColliderPackage gamePkg)
    {
        Log.cinput("yellow", "RpcStartAction_8");
        RadiationSource[] radArray = FindObjectsOfType<RadiationSource>();
        foreach (RadiationSource rad in radArray)
        {
            if (rad.RName == "Two" && rad.IsPickUpClear)
            {
                CoreAction.Get().TaskAction_8(gamePkg);
            }
        }
    }

    [ClientRpc] public override void RpcEndAction_8(GameColliderPackage gamePkg) { }

    [ClientRpc] public override void RpcStartAction_9(GameColliderPackage gamePkg)
    {
        CoreAction.Get().SetTaskArrowActive(gamePkg, true);
        AudioController.Get().Play(gamePkg.TaskItem.HintAudio);
        CoreAction.Get().StartAction_9(gamePkg);
    }

    [ClientRpc] public override void RpcTaskAction_9(GameColliderPackage gamePkg) { CoreAction.Get().TaskAction_9(gamePkg); }

    [ClientRpc] public override void RpcEndAction_9(GameColliderPackage gamePkg) { }

    [ClientRpc]
    public override void RpcStartAction_10(GameColliderPackage gamePkg)
    {
        CoreAction.Get().SetTaskArrowActive(gamePkg, true);
        AudioController.Get().Play(gamePkg.TaskItem.HintAudio);
        CoreAction.Get().StartAction_10(gamePkg);
    }

    [ClientRpc] public override void RpcTaskAction_10(GameColliderPackage gamePkg)
    {
        Log.cinput("yellow", "RpcStartAction_10");
        RadiationSource[] radArray = FindObjectsOfType<RadiationSource>();
        foreach (RadiationSource rad in radArray)
        {
            if (rad.RName == "Two" && rad.IsShovelClear)
            {
                CoreAction.Get().TaskAction_10(gamePkg);
            }
        }
    }

    [ClientRpc] public override void RpcEndAction_10(GameColliderPackage gamePkg) { }

    [ClientRpc] public override void RpcStartAction_11(GameColliderPackage gamePkg)
    {
        CoreAction.Get().SetTaskArrowActive(gamePkg, true);
        AudioController.Get().Play(gamePkg.TaskItem.HintAudio);
        CoreAction.Get().StartAction_11(gamePkg);
    }

    [ClientRpc] public override void RpcTaskAction_11(GameColliderPackage gamePkg) { CoreAction.Get().TaskAction_11(gamePkg); }

    [ClientRpc] public override void RpcEndAction_11(GameColliderPackage gamePkg) { }

    [ClientRpc]
    public override void RpcStartAction_12(GameColliderPackage gamePkg)
    {
        CoreAction.Get().SetTaskArrowActive(gamePkg, true);
        AudioController.Get().Play(gamePkg.TaskItem.HintAudio);
        CoreAction.Get().StartAction_12(gamePkg);
    }

    [ClientRpc] public override void RpcTaskAction_12(GameColliderPackage gamePkg) { CoreAction.Get().TaskAction_12(gamePkg); }

    [ClientRpc] public override void RpcEndAction_12(GameColliderPackage gamePkg) { }

    [ClientRpc] public override void RpcStartAction_13(GameColliderPackage gamePkg)
    {
        CoreAction.Get().SetTaskArrowActive(gamePkg, true);
        AudioController.Get().Play(gamePkg.TaskItem.HintAudio);
        CoreAction.Get().StartAction_13(gamePkg);
    }

    [ClientRpc] public override void RpcTaskAction_13(GameColliderPackage gamePkg) 
    {
        CoreAction.Get().TaskAction_13(gamePkg);
    }

    [ClientRpc] public override void RpcEndAction_13(GameColliderPackage gamePkg) { }

    [ClientRpc]
    public override void RpcStartActionWait(GameColliderPackage gamePkg)
    {
        CoreAction.Get().SetTaskArrowActive(gamePkg, true);
        AudioController.Get().Play(gamePkg.TaskItem.HintAudio);
        CoreAction.Get().StartActionWait(gamePkg);
    }

    [ClientRpc] public override void RpcTaskActionWait(GameColliderPackage gamePkg) { CoreAction.Get().TaskActionWait(gamePkg); }

    [ClientRpc] public override void RpcEndActionWait(GameColliderPackage gamePkg) { CoreAction.Get().EndActionWait(gamePkg); }
}
