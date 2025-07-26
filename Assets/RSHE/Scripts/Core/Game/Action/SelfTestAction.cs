using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> ×Ô²âÄ£Ê½ </summary>
public class SelfTestAction : BaseModeAction, IGameAction
{
    public void TaskOneStartAction() 
    {
        Log.cinput("yellow", "@@ SelfTestAction TaskOneStartAction..");
    }

    public void TaskOneEndAction() 
    {
        Log.cinput("yellow", "@@ SelfTestAction TaskOneEndAction..");
    }
}
