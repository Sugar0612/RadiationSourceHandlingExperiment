using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBase: NetworkBehaviour
{
    public virtual bool CheckTask_1() { return true; }
    public virtual bool CheckTask_2(NetworkPropsCollider collider, ref List<float> valueList) { return true; }
    public virtual bool CheckTask_3() { return true; }
    public virtual bool CheckTask_4() { return true; }
    public virtual bool CheckTask_5() { return true; }
    public virtual bool CheckTask_6() { return true; }
    public virtual bool CheckTask_7() { return true; }
    public virtual bool CheckTask_8() { return true; }
    public virtual bool CheckTask_9() { return true; }
    public virtual bool CheckTask_10() { return true; }
    public virtual bool CheckTask_11() { return true; }
    public virtual bool CheckTask_12() { return true; }
    public virtual bool CheckTask_13() { return true; }
}
