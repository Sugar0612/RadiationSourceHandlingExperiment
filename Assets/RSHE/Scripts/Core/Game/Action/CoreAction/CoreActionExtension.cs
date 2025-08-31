using Mirror;
using System;
using System.Linq;
using Unity.VisualScripting;
using static Unity.XR.PXR.ShapesRecognizer;

public partial class CoreAction : NetworkBehaviour
{

    /// <summary> Task one start. </summary>
    public void StartAction_1(GameColliderPackage gamePkg, Action callback = null)
    {
    }

    /// <summary> task one trigger collider. </summary>
    public void TaskAction_1(GameColliderPackage gamePkg, Action callback = null)
    {
        Log.cinput("yellow", "TaskAction_1");
        VRNetworkPlayerController ctrl = gamePkg?.VRPlayerCtrl.GetComponent<VRNetworkPlayerController>();

        if (ctrl)
        {
            PlayerWearPanel wearPanel = FindObjectOfType<PlayerWearPanel>();
            if (wearPanel && ctrl.WStatus == VRNetworkPlayerController.WearStatus.NoWear)
            {
                wearPanel.SetActive(true);
                wearPanel.Wearing(ctrl, () => 
                {
                    if (gamePkg != null)
                    {
                        TaskName taskNameEnum = (TaskName)Enum.Parse(typeof(TaskName), gamePkg?.TaskItem.taskName);
                        bool canGoOn = true;
                        TaskCondition condition = gamePkg.TaskItem.conditions.Find(x => x.Identity == ctrl.identity);

                        if (condition != null && condition.HoldingItemsIsEmpty())
                            condition.IsFinished = true;

                        foreach (var item in gamePkg.TaskItem.conditions)
                            canGoOn = canGoOn & item.IsFinished;

                        if (canGoOn)
                            GameSteps.Get().SetTaskFinished(taskNameEnum);

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
            TaskName taskNameEnum = (TaskName)Enum.Parse(typeof(TaskName), gamePkg?.TaskItem.taskName);
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
                GameSteps.Get().SetTaskFinished(taskNameEnum);

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
            TaskName taskNameEnum = (TaskName)Enum.Parse(typeof(TaskName), gamePkg?.TaskItem.taskName);
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
                GameSteps.Get().SetTaskFinished(taskNameEnum);

            HostIssuesTheGoNext(gamePkg, canGoOn);
        }
        callback?.Invoke();
    }

    /// <summary> task 3 end. </summary>
    public void EndAction_3(GameColliderPackage gamePkg, Action callback = null) { }

    /// <summary> Task 4 start. </summary>
    public void StartAction_4(GameColliderPackage gamePkg, Action callback = null) { }

    /// <summary> task 4 trigger collider. </summary>
    public void TaskAction_4(GameColliderPackage gamePkg, Action callback = null)
    {
        if (gamePkg != null)
        {
            Log.cinput("yellow", "TaskAction_4");
            TaskName taskNameEnum = (TaskName)Enum.Parse(typeof(TaskName), gamePkg?.TaskItem.taskName);
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
                GameSteps.Get().SetTaskFinished(taskNameEnum);

            HostIssuesTheGoNext(gamePkg, canGoOn);
        }
        callback?.Invoke();
    }

    /// <summary> task 4 end. </summary>
    public void EndAction_4(GameColliderPackage gamePkg, Action callback = null) { }


    /// <summary> Task 5 start. </summary>
    public void StartAction_5(GameColliderPackage gamePkg, Action callback = null) { }

    /// <summary> task 5 trigger collider. </summary>
    public void TaskAction_5(GameColliderPackage gamePkg, Action callback = null) 
    {
        if (gamePkg != null)
        {
            Log.cinput("yellow", "TaskAction_5");
            TaskName taskNameEnum = (TaskName)Enum.Parse(typeof(TaskName), gamePkg?.TaskItem.taskName);
            GameSteps.Get().SetTaskFinished(taskNameEnum);
            HostIssuesTheGoNext(gamePkg, true);
        }
        callback?.Invoke();
    }

    /// <summary> task 5 end. </summary>
    public void EndAction_5(GameColliderPackage gamePkg, Action callback = null) { }


    /// <summary> Task 6 start. </summary>
    public void StartAction_6(GameColliderPackage gamePkg, Action callback = null) { }

    /// <summary> task 6 trigger collider. </summary>
    public void TaskAction_6(GameColliderPackage gamePkg, Action callback = null)
    {
        if (gamePkg != null)
        {
            Log.cinput("yellow", "TaskAction_6");
            TaskName taskNameEnum = (TaskName)Enum.Parse(typeof(TaskName), gamePkg?.TaskItem.taskName);
            GameSteps.Get().SetTaskFinished(taskNameEnum);
            HostIssuesTheGoNext(gamePkg, true);
        }
        callback?.Invoke();
    }

    /// <summary> task 6 end. </summary>
    public void EndAction_6(GameColliderPackage gamePkg, Action callback = null) { }


    /// <summary> Task 7 start. </summary>
    public void StartAction_7(GameColliderPackage gamePkg, Action callback = null) { }

    /// <summary> task 7 trigger collider. </summary>
    public void TaskAction_7(GameColliderPackage gamePkg, Action callback = null)
    {
        if (gamePkg != null)
        {
            Log.cinput("yellow", "TaskAction_7");
            TaskName taskNameEnum = (TaskName)Enum.Parse(typeof(TaskName), gamePkg?.TaskItem.taskName);
            GameSteps.Get().SetTaskFinished(taskNameEnum);
            HostIssuesTheGoNext(gamePkg, true);
        }
        callback?.Invoke();
    }

    /// <summary> task 7 end. </summary>
    public void EndAction_7(GameColliderPackage gamePkg, Action callback = null) { }


    /// <summary> Task 8 start. </summary>
    public void StartAction_8(GameColliderPackage gamePkg, Action callback = null) { }

    /// <summary> task 8 trigger collider. </summary>
    public void TaskAction_8(GameColliderPackage gamePkg, Action callback = null)
    {
        if (gamePkg != null)
        {
            Log.cinput("yellow", "TaskAction_8");
            TaskName taskNameEnum = (TaskName)Enum.Parse(typeof(TaskName), gamePkg?.TaskItem.taskName);
            GameSteps.Get().SetTaskFinished(taskNameEnum);
            HostIssuesTheGoNext(gamePkg, true);
        }
        callback?.Invoke();
    }

    /// <summary> task 8 end. </summary>
    public void EndAction_8(GameColliderPackage gamePkg, Action callback = null) { }


    /// <summary> Task 9 start. </summary>
    public void StartAction_9(GameColliderPackage gamePkg, Action callback = null) { }

    /// <summary> task 9 trigger collider. </summary>
    public void TaskAction_9(GameColliderPackage gamePkg, Action callback = null)
    {
        if (gamePkg != null)
        {
            Log.cinput("yellow", "TaskAction_9");
            TaskName taskNameEnum = (TaskName)Enum.Parse(typeof(TaskName), gamePkg?.TaskItem.taskName);
            GameSteps.Get().SetTaskFinished(taskNameEnum);
            HostIssuesTheGoNext(gamePkg, true);
        }
        callback?.Invoke();
    }

    /// <summary> task 9 end. </summary>
    public void EndAction_9(GameColliderPackage gamePkg, Action callback = null) { }


    /// <summary> Task 10 start. </summary>
    public void StartAction_10(GameColliderPackage gamePkg, Action callback = null) { }

    /// <summary> task 10 trigger collider. </summary>
    public void TaskAction_10(GameColliderPackage gamePkg, Action callback = null)
    {
        if (gamePkg != null)
        {
            Log.cinput("yellow", "TaskAction_10");
            TaskName taskNameEnum = (TaskName)Enum.Parse(typeof(TaskName), gamePkg?.TaskItem.taskName);
            GameSteps.Get().SetTaskFinished(taskNameEnum);
            HostIssuesTheGoNext(gamePkg, true);
        }
        callback?.Invoke();
    }

    /// <summary> task 10 end. </summary>
    public void EndAction_10(GameColliderPackage gamePkg, Action callback = null) { }


    /// <summary> Task 11 start. </summary>
    public void StartAction_11(GameColliderPackage gamePkg, Action callback = null) { }

    /// <summary> task 11 trigger collider. </summary>
    public void TaskAction_11(GameColliderPackage gamePkg, Action callback = null)
    {
        if (gamePkg != null)
        {
            Log.cinput("yellow", "TaskAction_11");
            TaskName taskNameEnum = (TaskName)Enum.Parse(typeof(TaskName), gamePkg?.TaskItem.taskName);
            GameSteps.Get().SetTaskFinished(taskNameEnum);
            HostIssuesTheGoNext(gamePkg, true);
        }
        callback?.Invoke();
    }

    /// <summary> task 11 end. </summary>
    public void EndAction_11(GameColliderPackage gamePkg, Action callback = null) { }


    /// <summary> Task 12 start. </summary>
    public void StartAction_12(GameColliderPackage gamePkg, Action callback = null) { }

    /// <summary> task 12 trigger collider. </summary>
    public void TaskAction_12(GameColliderPackage gamePkg, Action callback = null)
    {
        if (gamePkg != null)
        {
            Log.cinput("yellow", "TaskAction_12");
            TaskName taskNameEnum = (TaskName)Enum.Parse(typeof(TaskName), gamePkg?.TaskItem.taskName);
            GameSteps.Get().SetTaskFinished(taskNameEnum);
            HostIssuesTheGoNext(gamePkg, true);
        }
        callback?.Invoke();
    }

    /// <summary> task 12 end. </summary>
    public void EndAction_12(GameColliderPackage gamePkg, Action callback = null) { }


    /// <summary> Task 13 start. </summary>
    public void StartAction_13(GameColliderPackage gamePkg, Action callback = null) { }

    /// <summary> task 13 trigger collider. </summary>
    public void TaskAction_13(GameColliderPackage gamePkg, Action callback = null)
    {
        VRNetworkPlayerController ctrl = gamePkg?.VRPlayerCtrl.GetComponent<VRNetworkPlayerController>();
        if (gamePkg != null && ctrl)
        {
            TaskName taskNameEnum = (TaskName)Enum.Parse(typeof(TaskName), gamePkg?.TaskItem.taskName);
            if (!GameSteps.Get().IsCheckTaskFinished(taskNameEnum))
            {
                Log.cinput("yellow", "In TaskAction_13");
                bool canGoOn = true;
                TaskCondition condition = gamePkg.TaskItem.conditions.Find(x => x.Identity == ctrl.identity);

                if (condition != null && condition.HoldingItemsIsEmpty())
                    condition.IsFinished = true;

                foreach (var item in gamePkg.TaskItem.conditions)
                    canGoOn = canGoOn & item.IsFinished;

                if (canGoOn)
                    GameSteps.Get().SetTaskFinished(taskNameEnum);

                Timer.Delay(10.0f, () => { HostIssuesTheGoNext(gamePkg, canGoOn); });
            }
        }
    }

    /// <summary> task 13 end. </summary>
    public void EndAction_13(GameColliderPackage gamePkg, Action callback = null) { }

    /// <summary> Task wait start. </summary>
    public void StartActionWait(GameColliderPackage gamePkg, Action callback = null) { }

    /// <summary> task wait collider. </summary>
    public void TaskActionWait(GameColliderPackage gamePkg, Action callback = null)
    {
        VRNetworkPlayerController ctrl = gamePkg?.VRPlayerCtrl.GetComponent<VRNetworkPlayerController>();
        if (gamePkg != null && ctrl)
        {
            TaskName taskNameEnum = (TaskName)Enum.Parse(typeof(TaskName), gamePkg?.TaskItem.taskName);
            if (!GameSteps.Get().IsCheckTaskFinished(taskNameEnum))
            {
                Log.cinput("yellow", "In TaskActionWait");
                bool canGoOn = true;
                TaskCondition condition = gamePkg.TaskItem.conditions.Find(x => x.Identity == ctrl.identity);

                if (condition != null && condition.HoldingItemsIsEmpty())
                    condition.IsFinished = true;

                foreach (var item in gamePkg.TaskItem.conditions)
                    canGoOn = canGoOn & item.IsFinished;

                if (canGoOn)
                    GameSteps.Get().SetTaskFinished(taskNameEnum);                

                HostIssuesTheGoNext(gamePkg, canGoOn);
            }
        }
    }

    /// <summary> task wait end. </summary>
    public void EndActionWait(GameColliderPackage gamePkg, Action callback = null) { } // gameObject.SetColliderEnable(false); }
}