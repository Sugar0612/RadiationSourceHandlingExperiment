using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class ClientSender : NetworkBehaviour
{
    public override void OnStartLocalPlayer()
    {
        if (isLocalPlayer)
        {
            Log.cinput("green", "ClientSender: OnStartLocalPlayer called");
            SendDeviceIDToServer();
        }
    }

    private void SendDeviceIDToServer()
    {
        Log.cinput("green", "ClientSender: Sending device ID to server");
        string deviceID = SystemInfo.deviceUniqueIdentifier;
        
        MirrorMsg msg = new MirrorMsg
        {
            deviceID = deviceID
        };

        NetworkClient.Send(msg);
    }
}
