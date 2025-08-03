using UnityEngine;
using Mirror;
using System.Collections;
using TMPro;

public class VRExitWindow : WinBase
{
    public TMP_Text text;
    public void OnClickedExitButton()
    {
        StartCoroutine(_SendDisconnectAndQuit());
    }

    private IEnumerator _SendDisconnectAndQuit()
    {
        // 发送断开消息
        StartCoroutine(Config.Get().GetLocalIdentity(arg => 
        {
            MirrorDisConnMsg msg = new MirrorDisConnMsg { Identity = arg };
            NetworkClient.Send(msg);
        }));

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

    public void ShowText(string tx)
    {
        text.text = tx;
    }
}