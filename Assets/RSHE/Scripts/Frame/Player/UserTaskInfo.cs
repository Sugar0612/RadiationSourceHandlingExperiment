using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UserTaskInfoItem
{
    [SerializeField]
    public string IndentityName = "";

    [SerializeField]
    public string Info = "";
}


public class UserTaskInfo : MonoBehaviour
{
    public List<UserTaskInfoItem> InfoList = new List<UserTaskInfoItem>();
}