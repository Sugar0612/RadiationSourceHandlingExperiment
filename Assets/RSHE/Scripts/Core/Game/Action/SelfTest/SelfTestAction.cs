using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> ×Ô²âÄ£Ê½ </summary>
public class SelfTestAction : ActionBase
{
    public override void StartAction_1(GameColliderPackage gamePkg) 
    {
        CoreAction.Get().SetTaskArrowActive(gamePkg, false);
        CoreAction.Get().StartAction_1(gamePkg); 
    }

    public override void TaskAction_1(GameColliderPackage gamePkg) { CoreAction.Get().TaskAction_1(gamePkg); }
     
    public override void EndAction_1(GameColliderPackage gamePkg) { CoreAction.Get().EndAction_1(gamePkg); }

    public override void StartAction_2(GameColliderPackage gamePkg) 
    {
        CoreAction.Get().SetTaskArrowActive(gamePkg, false);
        CoreAction.Get().StartAction_2(gamePkg); 
    }

    public override void TaskAction_2(GameColliderPackage gamePkg) { CoreAction.Get().TaskAction_2(gamePkg); }

    public override void EndAction_2(GameColliderPackage gamePkg) { CoreAction.Get().StartAction_2(gamePkg); }

    public override void StartAction_3(GameColliderPackage gamePkg) { CoreAction.Get().StartAction_3(gamePkg); }

    public override void TaskAction_3(GameColliderPackage gamePkg) { CoreAction.Get().TaskAction_3(gamePkg); }

    public override void EndAction_3(GameColliderPackage gamePkg) { CoreAction.Get().StartAction_3(gamePkg); }
}
