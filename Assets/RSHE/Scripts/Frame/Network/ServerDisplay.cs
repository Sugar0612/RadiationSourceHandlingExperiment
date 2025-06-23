using System.Collections;
using System.Collections.Generic;
using Mirror;
using TMPro;
using UnityEngine;

public class ServerDisplay : MonoBehaviour
{
    public TMP_Text deviceIDText; // 在Inspector中拖拽赋值

    void Start()
    { 
        // UIController.Get().ShowWindows((StaticGlobalVar.isPicoDevice == 0) ? EWindowType.MainWindow : EWindowType.VRJoinWindow);
        if (StaticGlobalVar.isPicoDevice == 0)
        {
            MainWindow mainWindow = UIController.Get().GetWindow<MainWindow>(EWindowType.MainWindow) as MainWindow;
            mainWindow.OnClickHostButton();
        }

        Log.cinput("green", "ServerDisplay: Start called");
        if (NetworkServer.active)
        {
            Log.cinput("green", "ServerDisplay: NetworkServer is active, registering handler");
            NetworkServer.RegisterHandler<MirrorMsg>(OnDeviceIDReceived);
            //NetworkServer.UnregisterHandler<MirrorMsg>();
        }
    }

    // 服务器收到设备ID时的处理
    private void OnDeviceIDReceived(NetworkConnection conn, MirrorMsg msg)
    {
        Log.cinput("green", $"ServerDisplay: Received device ID from client: {msg.deviceID}");
        // 在主线程更新UI
        UnityMainThreadDispatcher.Instance().Enqueue(() => 
        {
            Log.cinput("green", "ServerDisplay: Updating deviceIDText");
            deviceIDText.text += $"Client connected!\nDevice ID: {msg.deviceID}\n";
        });
    }
}
