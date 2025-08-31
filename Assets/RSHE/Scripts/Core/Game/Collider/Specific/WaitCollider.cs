using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static Unity.XR.PXR.ShapesRecognizer;

public class WaitCollider : NetworkBehaviour
{
    GameTaskItem _task;

    public void Start()
    {
        _task = gameObject.GetComponentInParent<GameTaskItem>();
    }

    [ServerCallback]
    public void OnTriggerStay(Collider other)
    {
        VRNetworkPlayerController ctrl =
                other.GetComponentInParent<VRNetworkPlayerController>();

        if (ctrl && _task && GameSteps.Get().isTopTask)
        {
            GameColliderPackage gamePkg = new GameColliderPackage()
            {
                VRPlayerCtrl = ctrl,
                TaskItem = _task,
            };
            _task.OnTask?.Invoke(gamePkg);
        }
    }

    [ServerCallback]
    public void OnTriggerExit(Collider other)
    {
        VRNetworkPlayerController ctrl =
                other.GetComponentInParent<VRNetworkPlayerController>();
        if (ctrl)
        {
            GameColliderPackage gamePkg = new GameColliderPackage()
            {
                VRPlayerCtrl = ctrl,
                TaskItem = _task,
            };
            RpcCancelTaskFinishedItem(gamePkg);
        }
    }

    [ClientRpc]
    void RpcCancelTaskFinishedItem(GameColliderPackage gamePkg)
    {
        if (gamePkg != null)
        {
            VRNetworkPlayerController ctrl = gamePkg.VRPlayerCtrl;
            TaskName taskNameEnum = (TaskName)Enum.Parse(typeof(TaskName), _task.taskName);
            if (!GameSteps.Get().IsCheckTaskFinished(taskNameEnum))
            {
                // Log.cinput("yellow", "WaitCollider OnTriggerExit");
                bool canGoOn = true;
                TaskCondition condition = _task.conditions.Find(x => x.Identity == ctrl.identity);

                if (condition != null && condition.HoldingItemsIsEmpty())
                    condition.IsFinished = false;

                foreach (var item in _task.conditions)
                    canGoOn = canGoOn & item.IsFinished;
            }
        }
    }
}
