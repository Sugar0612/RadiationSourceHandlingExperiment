using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropBase : NetworkBehaviour, IPropMethod
{
    /// <summary> 拿起时 </summary>
    public virtual void OnPickUp() { }

    /// <summary> 放下时 </summary>
    public virtual void OnLetGo() { }
}
