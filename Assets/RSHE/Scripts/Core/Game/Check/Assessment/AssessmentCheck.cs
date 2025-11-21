using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Transporter;

public class AssessmentCheck : CheckBase
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

        VRNetworkPlayerController vrCtrl = PlayerManager.Get().GetPlayer(identity);
        if (valueList.Count == 0 || wrongval != -1.0f)
        {
            //vrCtrl?.TargetPrompt(vrCtrl.connectionToClient, PromptType.DataError);
            Scorer.Get().Deduction(taskName, vrCtrl.identity, GameSteps.Get().currTask.fraction, PromptType.DataError);
            valueList.Clear();
            return false;
        }

        foreach (TaskName task in Enum.GetValues(typeof(TaskName)))
        {
            if (task == taskName)
                break;

            if (!GameSteps.Get().IsCheckTaskFinished(task))
            {
                Scorer.Get().Deduction(taskName, vrCtrl.identity, GameSteps.Get().currTask.fraction, PromptType.TaskOrderWrong);
                //vrCtrl?.TargetPrompt(vrCtrl.connectionToClient, PromptType.TaskOrderWrong);
                // valueList.Clear();
                return false;
            }
        }

        return true;
    }

    public override bool TCloseActionCheck(TaskName[] closeTaskArray, EIdentity identity)
    {
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
                // whoClickedButton?.TargetPrompt(whoClickedButton.connectionToClient, PromptType.TaskOrderWrong);
                Scorer.Get().Deduction(targetName, whoClickedButton.identity, GameSteps.Get().currTask.fraction, PromptType.TaskOrderWrong);
            }
        }

        return targetName != TaskName.T13;
    }

    public override bool InspectionSteps(Collider propCollider, TaskName[] testTaskArray)
    {
        TaskName targetTaskName = TaskName.T13;
        foreach (TaskName task in testTaskArray)
        {
            if (!GameSteps.Get().IsCheckTaskFinished(task))
            {
                targetTaskName = task;
                break;
            }
        }

        Detector detector = propCollider.GetComponentInParent<Detector>();
        PollutionDetector pollutionDetector = propCollider.GetComponentInParent<PollutionDetector>();
        if (detector == null && pollutionDetector == null)
        {
            NetworkPropsCollider propNetCollider = propCollider.GetComponentInParent<NetworkPropsCollider>();
            if (propNetCollider != null)
            {
                VRNetworkPlayerController player = PlayerManager.Get().GetPlayer(propNetCollider.WhoHeld);
                // player?.TargetPrompt(player.connectionToClient, PromptType.PropWrong);
                Scorer.Get().Deduction(targetTaskName, player.identity, GameSteps.Get().currTask.fraction, PromptType.PropWrong);
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
            // whoClickedButton?.TargetPrompt(whoClickedButton.connectionToClient, wrongType);
            Scorer.Get().Deduction(TaskName.T7, whoClickedButton.identity, GameSteps.Get().currTask.fraction, wrongType);
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
            // whoClickedButton?.TargetPrompt(whoClickedButton.connectionToClient, wrongType);
            Scorer.Get().Deduction(TaskName.T12, whoClickedButton.identity, GameSteps.Get().currTask.fraction, wrongType);
            return false;
        }
        return true;
    }
}
