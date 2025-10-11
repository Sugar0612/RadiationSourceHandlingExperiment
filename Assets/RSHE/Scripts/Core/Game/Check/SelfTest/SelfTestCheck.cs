using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfTestCheck : CheckBase
{
    public override bool CheckTask_2(NetworkPropsCollider propCollider, ref List<float> valueList)
    {
        Log.cinput("red", "@@ SelfTest T2Check Enter.");
        float wrongval = -1.0f;
        foreach (float val in valueList)
        {
            if (val < 0.10f || val > 0.30f)
            {
                wrongval = val;
                break;
            }
        }

        if (valueList.Count == 0 || wrongval != -1.0f)
        {
            VRNetworkPlayerController vrCtrl = PlayerManager.Get().GetPlayer(propCollider.WhoHeld);
            vrCtrl?.TargetPrompt(vrCtrl.connectionToClient, PromptType.DataError);
            valueList.Clear();
            return false;
        }

        foreach (TaskName task in Enum.GetValues(typeof(TaskName)))
        {
            if (task == TaskName.T2)
                break;

            if (!GameSteps.Get().IsCheckTaskFinished(task))
            {
                VRNetworkPlayerController vrCtrl = PlayerManager.Get().GetPlayer(propCollider.WhoHeld);
                vrCtrl?.TargetPrompt(vrCtrl.connectionToClient, PromptType.TaskOrderWrong);
                valueList.Clear();
                return false;
            }
        }

        Log.cinput("green", "@@ Task2 is passed!");
        return true;
    }
}
