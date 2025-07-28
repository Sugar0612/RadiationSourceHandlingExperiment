using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameAction
{
    /// <summary> Task one start. </summary>
    public void TaskOneStartAction();

    /// <summary> task one trigger collider. </summary>
    public void TaskOneAction(GameColliderPackage gamePkg);

    /// <summary> task one end. </summary>
    public void TaskOneEndAction();
}
