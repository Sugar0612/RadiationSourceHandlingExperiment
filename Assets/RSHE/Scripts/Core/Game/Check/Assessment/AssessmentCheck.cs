using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssessmentCheck : CheckBase
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
}
