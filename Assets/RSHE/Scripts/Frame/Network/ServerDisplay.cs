using System.Collections;
using System.Collections.Generic;
using Mirror;
using TMPro;
using UnityEngine;

public class ServerDisplay : MonoBehaviour
{
    public TMP_Text LogText; // 在Inspector中拖拽赋值

    void Start()
    {
        if (!Config.Get().PicoDevice)
        {
            MainWindow mainWindow = UIController.Get().GetWindow<MainWindow>(EWindowType.MainWindow) as MainWindow;
            mainWindow.OnClickHostButton();
        }

        if (NetworkServer.active)
        {
            // Log.cinput("green", "ServerDisplay: NetworkServer is active, registering handler");

            NetworkServer.RegisterHandler<MirrorConnMsg>(OnCliConnected);
            NetworkServer.RegisterHandler<MirrorDisConnMsg>(OnCliDisConnected);
        }
    }

    private void OnCliConnected(NetworkConnection conn, MirrorConnMsg msg)
        {
        UnityMainThreadDispatcher.Instance().Enqueue(() =>
        {
            LogText.text += $"Client connected! Device ID: {msg.deviceID}\n";

            UserWindow userWin = UIController.Get().GetWindow<UserWindow>(EWindowType.UserWindow) as UserWindow;
            userWin.SetItemState(msg.deviceID, EUserState.Online);
        });
    }

    private void OnCliDisConnected(NetworkConnection conn, MirrorDisConnMsg msg)
    {
        UnityMainThreadDispatcher.Instance().Enqueue(() =>
        {
            LogText.text += $"Client Disconnected! Device ID: {msg.deviceID}\n";

            UserWindow userWin = UIController.Get().GetWindow<UserWindow>(EWindowType.UserWindow) as UserWindow;
            userWin.SetItemState(msg.deviceID, EUserState.Offline);
        });
    }
}
