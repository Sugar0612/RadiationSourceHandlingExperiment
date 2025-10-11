using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TaskInspector
{
    CheckBase _checkBase;

    public TaskInspector()
    {
        _checkBase = GameCheckDispenser.Get().Dispenser(StaticGlobalVar.GameMode);
    }

    public bool T2Check(NetworkPropsCollider collider, ref List<float> valuelist)
    {
        Log.cinput("red", "@@ TaskInspector T2Check Enter.");
        return _checkBase.CheckTask_2(collider, ref valuelist);
    }
}
