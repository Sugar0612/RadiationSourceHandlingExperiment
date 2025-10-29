using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Transporter;

public class SelfTestCheck : CheckBase
{
    public override bool CheckRecordTask(float minVal, float maxVal, EIdentity identity, TaskName taskName, ref List<float> valueList)
    {
        float wrongval = -1.0f;
        foreach (float val in valueList)
        {
            if (val < minVal || val > maxVal)
            {
                wrongval = val;
                break;
            }
        }
        Log.cinput("red", $"wrongval: {wrongval}, identity: {identity}, taskName: {taskName}");
        VRNetworkPlayerController vrCtrl = PlayerManager.Get().GetPlayer(identity);
        if (valueList.Count == 0 || wrongval != -1.0f)
        {
            vrCtrl?.TargetPrompt(vrCtrl.connectionToClient, PromptType.DataError);
            valueList.Clear();
            return false;
        }

        foreach (TaskName task in Enum.GetValues(typeof(TaskName)))
        {
            if (task == taskName)
                break;

            if (!GameSteps.Get().IsCheckTaskFinished(task))
            {
                vrCtrl?.TargetPrompt(vrCtrl.connectionToClient, PromptType.TaskOrderWrong);
                // valueList.Clear();
                return false;
            }
        }

        return true;
    }

    //public override bool CheckTask_2(NetworkPropsCollider propCollider, ref List<float> valueList)
    //{
    //    float wrongval = -1.0f;
    //    foreach (float val in valueList)
    //    {
    //        if (val < 0.10f || val > 0.30f)
    //        {
    //            wrongval = val;
    //            break;
    //        }
    //    }

    //    if (valueList.Count == 0 || wrongval != -1.0f)
    //    {
    //        VRNetworkPlayerController vrCtrl = PlayerManager.Get().GetPlayer(propCollider.WhoHeld);
    //        vrCtrl?.TargetPrompt(vrCtrl.connectionToClient, PromptType.DataError);
    //        valueList.Clear();
    //        return false;
    //    }

    //    foreach (TaskName task in Enum.GetValues(typeof(TaskName)))
    //    {
    //        if (task == TaskName.T2)
    //            break;

    //        if (!GameSteps.Get().IsCheckTaskFinished(task))
    //        {
    //            VRNetworkPlayerController vrCtrl = PlayerManager.Get().GetPlayer(propCollider.WhoHeld);
    //            vrCtrl?.TargetPrompt(vrCtrl.connectionToClient, PromptType.TaskOrderWrong); 
    //            // valueList.Clear();
    //            return false;
    //        }
    //    }

    //    return true;
    //}

    //public override bool CheckTask_3(NetworkPropsCollider propCollider, ref List<float> valueList)
    //{
    //    float wrongval = -1.0f;
    //    foreach (float val in valueList)
    //    {
    //        if (val < 0.10f || val > 0.50f)
    //        {
    //            wrongval = val;
    //            break;
    //        }
    //    }

    //    if (valueList.Count == 0 || wrongval != -1.0f)
    //    {
    //        VRNetworkPlayerController vrCtrl = PlayerManager.Get().GetPlayer(propCollider.WhoHeld);
    //        vrCtrl?.TargetPrompt(vrCtrl.connectionToClient, PromptType.DataError);
    //        valueList.Clear();
    //        return false;
    //    }

    //    foreach (TaskName task in Enum.GetValues(typeof(TaskName)))
    //    {
    //        if (task == TaskName.T3)
    //            break;

    //        if (!GameSteps.Get().IsCheckTaskFinished(task))
    //        {
    //            VRNetworkPlayerController vrCtrl = PlayerManager.Get().GetPlayer(propCollider.WhoHeld);
    //            vrCtrl?.TargetPrompt(vrCtrl.connectionToClient, PromptType.TaskOrderWrong);
    //            // valueList.Clear();
    //            return false;
    //        }
    //    }

    //    return true;
    //}

    //public override bool CheckTask_4(NetworkPropsCollider propCollider, ref List<float> valueList)
    //{
    //    float wrongval = -1.0f;
    //    foreach (float val in valueList)
    //    {
    //        if (val < 0.10f || val > 1.0f)
    //        {
    //            wrongval = val;
    //            break;
    //        }
    //    }

    //    if (valueList.Count == 0 || wrongval != -1.0f)
    //    {
    //        VRNetworkPlayerController vrCtrl = PlayerManager.Get().GetPlayer(propCollider.WhoHeld);
    //        vrCtrl?.TargetPrompt(vrCtrl.connectionToClient, PromptType.DataError);
    //        valueList.Clear();
    //        return false;
    //    }

    //    foreach (TaskName task in Enum.GetValues(typeof(TaskName)))
    //    {
    //        if (task == TaskName.T4)
    //            break;

    //        if (!GameSteps.Get().IsCheckTaskFinished(task))
    //        {
    //            VRNetworkPlayerController vrCtrl = PlayerManager.Get().GetPlayer(propCollider.WhoHeld);
    //            vrCtrl?.TargetPrompt(vrCtrl.connectionToClient, PromptType.TaskOrderWrong);
    //            // valueList.Clear();
    //            return false;
    //        }
    //    }

    //    return true;
    //}

    public override bool TCloseActionCheck(TaskName[] closeTaskArray, EIdentity identity)
    {
        //Log.cinput("yellow", $"@@ closeTaskArray count: {closeTaskArray.Count()}");
        TaskName targetName = TaskName.T13;
        foreach (TaskName task in closeTaskArray)
        {
            if (!GameSteps.Get().IsCheckTaskFinished(task))
            {
                targetName = task;
                break;
            }
        }

        if (targetName != TaskName.T13)
        {
            if (!GameSteps.Get().CheckTaskGoRun(targetName))
            {
                VRNetworkPlayerController whoClickedButton = PlayerManager.Get().GetPlayer(identity);
                whoClickedButton?.TargetPrompt(whoClickedButton.connectionToClient, PromptType.TaskOrderWrong);
            }
        }

        return targetName != TaskName.T13;
    }

    public override bool InspectionSteps(Collider propCollider)
    {
        Detector detector = propCollider.GetComponentInParent<Detector>();
        PollutionDetector pollutionDetector = propCollider.GetComponentInParent<PollutionDetector>();
        if (detector == null && pollutionDetector == null)
        {
            NetworkPropsCollider propNetCollider = propCollider.GetComponentInParent<NetworkPropsCollider>();
            if (propNetCollider != null)
            {
                VRNetworkPlayerController player = PlayerManager.Get().GetPlayer(propNetCollider.WhoHeld);
                player?.TargetPrompt(player.connectionToClient, PromptType.PropWrong);
            }
            return false;
        }
        return true;
    }

    public override bool CheckTask_7(JarStatus p_JarStatus, EIdentity identity)
    {
        bool isClose = p_JarStatus == JarStatus.Close;
        bool isOrder = true; //GameSteps.Get().IsCheckTaskFinished(TaskName.T7);
        foreach (TaskName task in Enum.GetValues(typeof(TaskName)))
        {
            if (task == TaskName.T7)
                break;

            if (!GameSteps.Get().IsCheckTaskFinished(task))
                isOrder = false;
        }

        PromptType wrongType = PromptType.None;
        if (wrongType == PromptType.None && isOrder == false) wrongType = PromptType.TaskOrderWrong;
        else if (wrongType == PromptType.None && isClose == false) wrongType = PromptType.TActionWrong;

        if (wrongType != PromptType.None)
        {
            VRNetworkPlayerController whoClickedButton = PlayerManager.Get().GetPlayer(identity);
            whoClickedButton?.TargetPrompt(whoClickedButton.connectionToClient, wrongType);
            return false;
        }
        return true;
    }

    public override bool CheckTask_12(JarStatus p_JarStatus, EIdentity identity)
    {
        bool isClose = p_JarStatus == JarStatus.Close;
        bool isOrder = true; //GameSteps.Get().IsCheckTaskFinished(TaskName.T7);
        foreach (TaskName task in Enum.GetValues(typeof(TaskName)))
        {
            if (task == TaskName.T12)
                break;

            if (!GameSteps.Get().IsCheckTaskFinished(task))
                isOrder = false;
        }

        PromptType wrongType = PromptType.None;
        if (wrongType == PromptType.None && isOrder == false) wrongType = PromptType.TaskOrderWrong;
        else if (wrongType == PromptType.None && isClose == false) wrongType = PromptType.TActionWrong;

        if (wrongType != PromptType.None)
        {
            VRNetworkPlayerController whoClickedButton = PlayerManager.Get().GetPlayer(identity);
            whoClickedButton?.TargetPrompt(whoClickedButton.connectionToClient, wrongType);
            return false;
        }
        return true;
    }
}
