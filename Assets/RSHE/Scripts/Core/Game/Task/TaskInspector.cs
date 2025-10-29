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

    public bool CheckRecordTask(float minVal, float maxVal, EIdentity identity, TaskName taskName, ref List<float> valueList)
    {
        return _checkBase.CheckRecordTask(minVal, maxVal, identity, taskName, ref valueList);
    }

    public bool TCloseActionCheck(TaskName[] closeTaskArray, EIdentity identity)
    {
        return _checkBase.TCloseActionCheck(closeTaskArray, identity);
    }

    public bool InspectionSteps(Collider other)
    {
        return _checkBase.InspectionSteps(other);
    }

    public bool T7Check(JarStatus p_JarStatus, EIdentity identity)
    {
        return _checkBase.CheckTask_7(p_JarStatus, identity);
    }

    public bool T12Check(JarStatus p_JarStatus, EIdentity identity)
    {
        return _checkBase.CheckTask_12(p_JarStatus, identity);
    }
}
