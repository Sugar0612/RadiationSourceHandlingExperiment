using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class TaskCondition
{
    [Serializable]
    public class PropsPackage
    {
        /// <summary> 道具名字 </summary>
        public string pName;

        /// <summary> 道具数量 </summary>
        public int pCount = 0;
    }

    ///// <summary> 人物身份 </summary>
    public EIdentity identity = EIdentity.None;

    /// <summary> 持有物品以及数量列表 </summary>
    [SerializeField]
    public List<PropsPackage> HoldingItems = new List<PropsPackage>();
}
