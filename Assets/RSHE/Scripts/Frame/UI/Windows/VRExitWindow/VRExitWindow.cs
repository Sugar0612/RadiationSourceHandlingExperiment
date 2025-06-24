using UnityEngine;
using Mirror;
using System.Collections;

public class VRExitWindow : WinBase
{
    public void OnClickedExitButton()
    {
        StartCoroutine(_SendDisconnectAndQuit());
    }

    private IEnumerator _SendDisconnectAndQuit()
    {
        // 发送断开消息
        string deviceID = SystemInfo.deviceUniqueIdentifier;
        MirrorDisConnMsg msg = new MirrorDisConnMsg { deviceID = deviceID };
        NetworkClient.Send(msg);

        Log.cinput("yellow", "Disconnect notification sent");

        // 等待消息发送（至少1帧）
        yield return null;

        // 断开客户端
        NetworkManager.singleton.StopClient();

        // 等待断开完成
        yield return new WaitForSeconds(0.1f);

        // 退出应用
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}