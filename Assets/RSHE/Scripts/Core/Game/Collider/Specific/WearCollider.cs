using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WearCollider : NetworkBehaviour
{
    GameTaskItem _task;

    public void Start()
    {
        _task = gameObject.GetComponentInParent<GameTaskItem>();
    }

    [ServerCallback]
    public void OnTriggerEnter(Collider other)
    {
        VRNetworkPlayerController ctrl =
                other.GetComponentInParent<VRNetworkPlayerController>();

        if (ctrl && _task && ctrl.WStatus == VRNetworkPlayerController.WearStatus.NoWear)
        {
            GameColliderPackage gamePkg = new GameColliderPackage()
            {
                VRPlayerCtrl = ctrl,
                TaskItem = _task,
            };
            // _task.OnTask?.Invoke(gamePkg);
            DressingSequence(gamePkg);
        }      
    }

    [ServerCallback]
    public void OnTriggerExit(Collider other)
    {
        VRNetworkPlayerController ctrl =
                other.GetComponentInParent<VRNetworkPlayerController>();

        if (ctrl && ctrl.WStatus == VRNetworkPlayerController.WearStatus.Wearing)
        {
            ctrl.RpcSetWStatus(VRNetworkPlayerController.WearStatus.NoWear);
        }

        RpcOnTriggerExitEvent();
        //ctrl.WStatus = ;
    }

    void DressingSequence(GameColliderPackage gamePkg)
    {
        VRNetworkPlayerController ctrl = gamePkg?.VRPlayerCtrl.GetComponent<VRNetworkPlayerController>();

        if (ctrl)
        {
            PlayerWearPanel wearPanel = FindObjectOfType<PlayerWearPanel>();
            if (wearPanel && ctrl.WStatus == VRNetworkPlayerController.WearStatus.NoWear && wearPanel.workState == PlayerWearPanel.WearPanelState.Wait)
            {
                wearPanel.RpcSetActive(true);
                wearPanel.Wearing(ctrl, () =>
                {
                    ctrl.WStatus = VRNetworkPlayerController.WearStatus.Wore;
                    ctrl.RpcSetClothingActive(true);

                    if (gamePkg != null)
                    {
                        bool canGoOn = true;
                        TaskCondition condition = gamePkg.TaskItem.conditions.Find(x => x.Identity == ctrl.identity);

                        if (condition != null && condition.HoldingItemsIsEmpty())
                            condition.IsFinished = true;

                        foreach (var item in gamePkg.TaskItem.conditions)
                            canGoOn = canGoOn & item.IsFinished;

                        if (canGoOn && !GameSteps.Get().IsCheckTaskFinished(TaskName.T1))
                        {
                            _task.OnTask?.Invoke(gamePkg);
                        }
                    }
                });
            }
            else if (wearPanel && ctrl.WStatus == VRNetworkPlayerController.WearStatus.Wore)
            {
                if (!ctrl.isLocalPlayer)
                {
                    ctrl.RpcSetClothingActive(true);
                }
                wearPanel.RpcSetActive(false);
                wearPanel.RpcSetWorePanelActive(true);
            }
        }
    }

    [ClientRpc]
    void RpcOnTriggerExitEvent()
    {
        PlayerWearPanel wearPanel = FindObjectOfType<PlayerWearPanel>();
        if (wearPanel)
            wearPanel.workState = PlayerWearPanel.WearPanelState.Wait;
    }
}
