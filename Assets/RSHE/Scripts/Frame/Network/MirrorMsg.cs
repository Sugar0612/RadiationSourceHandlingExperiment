using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public struct  MirrorConnMsg : NetworkMessage
{
    public EIdentity Identity;
}

public struct MirrorDisConnMsg : NetworkMessage
{
    public EIdentity Identity;
}