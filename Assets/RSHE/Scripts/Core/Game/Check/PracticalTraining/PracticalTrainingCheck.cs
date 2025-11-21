using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Transporter;

public class PracticalTrainingCheck : CheckBase
{
    public override bool CheckRecordTask(float minVal, float maxVal, EIdentity identity, TaskName taskName, ref List<float> valueList)
    {
        foreach (TaskName task in Enum.GetValues(typeof(TaskName)))
        {
            if (task == taskName)
                break;

            if (!GameSteps.Get().IsCheckTaskFinished(task))
            {
                return false;
            }
        }
        return true;
    }

    //public override bool CheckTask_2(NetworkPropsCollider propCollider, ref List<float> valueList)
    //{
    //    foreach (TaskName task in Enum.GetValues(typeof(TaskName)))
    //    {
    //        if (task == TaskName.T2)
    //            break;

    //        if (!GameSteps.Get().IsCheckTaskFinished(task))
    //        {
    //            return false;
    //        }
    //    }

    //    return true;
    //}

    //public override bool CheckTask_3(NetworkPropsCollider propCollider, ref List<float> valueList)
    //{
    //    foreach (TaskName task in Enum.GetValues(typeof(TaskName)))
    //    {
    //        if (task == TaskName.T3)
    //            break;

    //        if (!GameSteps.Get().IsCheckTaskFinished(task))
    //        {
    //            return false;
    //        }
    //    }
    //    return true;
    //}

    //public override bool CheckTask_4(NetworkPropsCollider propCollider, ref List<float> valueList)
    //{
    //    foreach (TaskName task in Enum.GetValues(typeof(TaskName)))
    //    {
    //        if (task == TaskName.T4)
    //            break;

    //        if (!GameSteps.Get().IsCheckTaskFinished(task))
    //        {
    //            return false;
    //        }
    //    }
    //    return true;
    //}

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
                return false;
            }
        }

        return targetName != TaskName.T13;
    }

    public override bool InspectionSteps(Collider propCollider, TaskName[] testTaskArray)
    {
        Detector detector = propCollider.GetComponentInParent<Detector>();
        PollutionDetector pollutionDetector = propCollider.GetComponentInParent<PollutionDetector>();
        if (detector == null && pollutionDetector == null)
        {
            return false;
        }
        return true;
    }

    public override bool CheckTask_7(JarStatus p_JarStatus, EIdentity identity)
    {
        bool isClose = p_JarStatus == JarStatus.Close;
        bool isOrder = true;
        foreach (TaskName task in Enum.GetValues(typeof(TaskName)))
        {
            if (task == TaskName.T7)
                break;

            if (!GameSteps.Get().IsCheckTaskFinished(task))
                isOrder = false;
        }

        if (isClose == false || isOrder == false) return false;

        return true;
    }

    public override bool CheckTask_12(JarStatus p_JarStatus, EIdentity identity)
    {
        bool isClose = p_JarStatus == JarStatus.Close;
        bool isOrder = true;
        foreach (TaskName task in Enum.GetValues(typeof(TaskName)))
        {
            if (task == TaskName.T12)
                break;

            if (!GameSteps.Get().IsCheckTaskFinished(task))
                isOrder = false;
        }

        if (isClose == false || isOrder == false) return false;

        return true;
    }
}
