using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class TaskCondition
{
    ///// <summary> 人物身份 </summary>
    public EIdentity identity = EIdentity.None;

    /// <summary> 持有物品列表 </summary>
    public List<string> HoldingItems = new List<string>();
}
