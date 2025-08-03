using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class ClientSender : NetworkBehaviour
{
    public void Start()
    {
        if (isLocalPlayer)
        {
            SendDeviceIDToServer();
        }
    }

    private void SendDeviceIDToServer()
    {
        // Log.cinput("green", "ClientSender: Sending device ID to server");

        StartCoroutine(Config.Get().GetLocalIdentity(arg => 
        {
            MirrorConnMsg msg = new MirrorConnMsg() { Identity = arg };
            NetworkClient.Send(msg);
        }));
    }
}
