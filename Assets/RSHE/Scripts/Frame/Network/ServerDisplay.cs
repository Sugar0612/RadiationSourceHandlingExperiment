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

        if (NetworkServer.active)
        {
            Log.cinput("green", "ServerDisplay: NetworkServer is active, registering handler");
            NetworkServer.RegisterHandler<MirrorConnMsg>(OnCliConnected);
            NetworkServer.RegisterHandler<MirrorDisConnMsg>(OnCliDisConnected);
        }
    }

    private void OnCliConnected(NetworkConnection conn, MirrorConnMsg msg)
        {
        Log.cinput("yellow", $"Client connected: {msg.deviceID}");
        // 在主线程更新UI
        UnityMainThreadDispatcher.Instance().Enqueue(() =>
        {
            deviceIDText.text += $"Client connected!\nDevice ID: {msg.deviceID}\n";
        });
    }

    private void OnCliDisConnected(NetworkConnection conn, MirrorDisConnMsg msg)
    {
        Log.cinput("yellow", $"Client Disconnected: {msg.deviceID}");
        // 在主线程更新UI
        UnityMainThreadDispatcher.Instance().Enqueue(() =>
        {
            deviceIDText.text += $"Client dis connected!\nDevice ID: {msg.deviceID}\n";
        });
    }
}
