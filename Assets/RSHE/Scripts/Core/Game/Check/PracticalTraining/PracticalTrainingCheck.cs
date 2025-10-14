using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PracticalTrainingCheck : CheckBase
{
    public override bool CheckTask_2(NetworkPropsCollider propCollider, ref List<float> valueList)
    {
        foreach (TaskName task in Enum.GetValues(typeof(TaskName)))
        {
            if (task == TaskName.T2)
                break;

            if (!GameSteps.Get().IsCheckTaskFinished(task))
            {
                return false;
            }
        }

        return true;
    }

    public override bool CheckTask_3(NetworkPropsCollider propCollider, ref List<float> valueList)
    {
        foreach (TaskName task in Enum.GetValues(typeof(TaskName)))
        {
            if (task == TaskName.T3)
                break;

            if (!GameSteps.Get().IsCheckTaskFinished(task))
            {
                return false;
            }
        }
        return true;
    }

    public override bool CheckTask_4(NetworkPropsCollider propCollider, ref List<float> valueList)
    {
        foreach (TaskName task in Enum.GetValues(typeof(TaskName)))
        {
            if (task == TaskName.T4)
                break;

            if (!GameSteps.Get().IsCheckTaskFinished(task))
            {
                return false;
            }
        }
        return true;
    }

    public override bool CheckTask_5(TaskName[] closeTaskArray, out TaskName targetName)
    {
        targetName = TaskName.T13;
        foreach (TaskName task in closeTaskArray)
        {
            if (!GameSteps.Get().IsCheckTaskFinished(task))
            {
                targetName = task;
                break;
            }
        }
        return targetName != TaskName.T13;
    }

    public override bool CheckTask_6(Collider propCollider)
    {
        Detector detector = propCollider.GetComponentInParent<Detector>();
        PollutionDetector pollutionDetector = propCollider.GetComponentInParent<PollutionDetector>();
        if (detector == null && pollutionDetector == null)
        {
            return false;
        }
        return true;
    }
}
