using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Transporter;

public class CheckBase: NetworkBehaviour
{
    public virtual bool CheckTask_1() { return true; }
    public virtual bool CheckRecordTask(float minVal, float maxVal, EIdentity identity, TaskName taskName, ref List<float> valueList) { return true; }
    //public virtual bool CheckTask_2(NetworkPropsCollider collider, ref List<float> valueList) { return true; }
    //public virtual bool CheckTask_3(NetworkPropsCollider collider, ref List<float> valueList) { return true; }
    //public virtual bool CheckTask_4(NetworkPropsCollider collider, ref List<float> valueList) { return true; }
    public virtual bool TCloseActionCheck(TaskName[] closeTaskArray, EIdentity identity) { return true; }
    public virtual bool InspectionSteps(Collider propCollider) { return true; }
    public virtual bool CheckTask_7(JarStatus p_JarStatus, EIdentity identity) { return true; }
    public virtual bool CheckTask_12(JarStatus p_JarStatus, EIdentity identity) { return true; }
    public virtual bool CheckTask_13() { return true; }
}
