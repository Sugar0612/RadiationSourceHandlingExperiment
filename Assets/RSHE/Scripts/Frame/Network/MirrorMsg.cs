using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public struct  MirrorConnMsg : NetworkMessage
{
    public string deviceID;
}

public struct MirrorDisConnMsg : NetworkMessage
{
    public string deviceID;
}