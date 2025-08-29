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

        public PropsPackage Clone()
        {
            PropsPackage copy = new PropsPackage();
            copy.pName = this.pName;
            copy.pCount = this.pCount;
            return copy;
        }
    }

    ///// <summary> 人物身份 </summary>
    public EIdentity Identity = EIdentity.None;

    /// <summary> 触发部位 </summary>
    public EBodyParts BodyPart = EBodyParts.None;

    /// <summary> 持有物品以及数量列表 </summary>
    [SerializeField]
    public List<PropsPackage> HoldingItems = new List<PropsPackage>();

    /// <summary> 这个条件已经完成 </summary>
    public bool IsFinished = false;

    /// <summary> HoldingItem 是否为空或者其中的道具都使用结束可以进入 OnEndEvent阶段 </summary>
    public bool HoldingItemsIsEmpty() => HoldingItems == null || HoldingItems.Count == 0;

    public TaskCondition Clone()
    {
        TaskCondition copy = new TaskCondition();
        copy.Identity = this.Identity;
        copy.BodyPart = this.BodyPart;
        copy.HoldingItems = new List<PropsPackage>();

        foreach (PropsPackage pkg in this.HoldingItems)
        {
            copy.HoldingItems.Add(pkg.Clone());
        }
        return copy;
    }
}
