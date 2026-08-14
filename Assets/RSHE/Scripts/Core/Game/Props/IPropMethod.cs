using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPropMethod
{
    /// <summary> 拿起时 </summary>
    public void OnPickUp();

    /// <summary> 放下时 </summary>
    public void OnLetGo();

    /// <summary> 生成时 </summary>
    public void OnSpawn();
}
