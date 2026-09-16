using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropBase : NetworkBehaviour, IPropMethod
{
    /// <summary> 手部模型 </summary>
    [SerializeField] protected GameObject _handPose;

    /// <summary> 拿起时 </summary>
    public virtual void OnPickUp() { if (_handPose != null) _handPose.SetActive(true); }

    /// <summary> 放下时 </summary>
    public virtual void OnLetGo() { if (_handPose != null) _handPose.SetActive(false); }

    /// <summary> 生成时 </summary>
    public virtual void OnSpawn() { if (_handPose != null) _handPose.SetActive(true); }
}
