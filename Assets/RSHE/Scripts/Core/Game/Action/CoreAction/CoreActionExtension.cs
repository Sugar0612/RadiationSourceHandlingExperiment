using Mirror;
using System;

public partial class CoreAction : NetworkBehaviour
{

    /// <summary> Task one start. </summary>
    public void StartAction_1(GameColliderPackage gamePkg, Action callback = null)
    {
    }

    /// <summary> task one trigger collider. </summary>
    public void TaskAction_1(GameColliderPackage gamePkg, Action callback = null)
    {
        VRNetworkPlayerController ctrl = gamePkg?.VRPlayerCtrl.GetComponent<VRNetworkPlayerController>();

        if (ctrl)
        {
            PlayerWearPanel wearPanel = FindObjectOfType<PlayerWearPanel>();
            if (wearPanel && ctrl.WStatus != VRNetworkPlayerController.WearStatus.Wore)
            {
                wearPanel.SetActive(true);
                wearPanel.Wearing(ctrl, () => 
                {
                    if (gamePkg != null)
                    {
                        bool canGoOn = true;
                        TaskCondition condition = gamePkg.TaskItem.conditions.Find(x => x.Identity == ctrl.identity);

                        if (condition != null && condition.HoldingItemsIsEmpty())
                            condition.IsFinished = true;

                        foreach (var item in gamePkg.TaskItem.conditions)
                            canGoOn = canGoOn & item.IsFinished;

                        HostIssuesTheGoNext(gamePkg, canGoOn);
                    }

                    callback?.Invoke();
                });
            }
            else if (wearPanel && ctrl.WStatus == VRNetworkPlayerController.WearStatus.Wore)
            {
                if (!ctrl.isLocalPlayer)
                {
                    ctrl.hat.SetRendererEnable(true);
                    ctrl.clothes.SetRendererEnable(true);
                }
                wearPanel.SetActive(false);
                wearPanel.SetWorePanelActive(true);
            }
        }
    }

    /// <summary> task one end. </summary>
    public void EndAction_1(GameColliderPackage gamePkg, Action callback = null) { }

    /// <summary> Task 2 start. </summary>
    public void StartAction_2(GameColliderPackage gamePkg, Action callback = null) { }

    /// <summary> task 2 trigger collider. </summary>
    public void TaskAction_2(GameColliderPackage gamePkg, Action callback = null)
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

            HostIssuesTheGoNext(gamePkg, canGoOn);
        }
        callback?.Invoke();
    }

    /// <summary> task 2 end. </summary>
    public void EndAction_2(GameColliderPackage gamePkg, Action callback = null) { }

    /// <summary> Task 3 start. </summary>
    public void StartAction_3(GameColliderPackage gamePkg, Action callback = null) { }

    /// <summary> task 3 trigger collider. </summary>
    public void TaskAction_3(GameColliderPackage gamePkg, Action callback = null)
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

            HostIssuesTheGoNext(gamePkg, canGoOn);
        }
        callback?.Invoke();
    }

    /// <summary> task 3 end. </summary>
    public void EndAction_3(GameColliderPackage gamePkg, Action callback = null) { }
}