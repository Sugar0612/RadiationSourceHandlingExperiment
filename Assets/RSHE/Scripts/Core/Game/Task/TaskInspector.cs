using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using Unity.VisualScripting;
using UnityEngine;
using static Transporter;

public class TaskInspector
{
    CheckBase _checkBase;

    public TaskInspector()
    {
        _checkBase = GameCheckDispenser.Get().Dispenser(StaticGlobalVar.GameMode);
    }

    public bool T2Check(NetworkPropsCollider collider, ref List<float> valuelist)
    {
        return _checkBase.CheckTask_2(collider, ref valuelist);
    }

    public bool T3Check(NetworkPropsCollider collider, ref List<float> valuelist)
    {
        return _checkBase.CheckTask_3(collider, ref valuelist);
    }

    public bool T4Check(NetworkPropsCollider collider, ref List<float> valuelist)
    {
        return _checkBase.CheckTask_4(collider, ref valuelist);
    }

    public bool T5Check(TaskName[] closeTaskArray, EIdentity identity)
    {
        return _checkBase.CheckTask_5(closeTaskArray, identity);
    }

    public bool T6Check(Collider other)
    {
        return _checkBase.CheckTask_6(other);
    }

    public bool T7Check(JarStatus p_JarStatus, EIdentity identity)
    {
        return _checkBase.CheckTask_7(p_JarStatus, identity);
    }
}
