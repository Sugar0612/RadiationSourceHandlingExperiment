
using Mirror;
using UnityEngine;
using UnityEngine.UI;

public class MainWindow : WinBase
{
    public GameObject vrPanel;
    public GameObject normalPanel;

    [Header("Control")]

    // 主机按钮
    private Button hostButton;

    public override void Start()
    {
        base.Start();
        
        vrPanel.SetActive(Config.Get().PicoDevice == true);
        normalPanel.SetActive(Config.Get().PicoDevice == false);

        if (Config.Get().PicoDevice == false)
        {
            gameObject.TryFindAndSetStatus("HostButton", true, out hostButton);
            //OnClickHostButton();
        }
    }

    public void OnClickHostButton()
    {
        Log.cinput("green", "Host Button Clicked");
        NetworkManager.singleton.StartHost();
        StaticGlobalVar.NetworkDiscovery.AdvertiseServer();

        UIController.Get().ShowWindows(EWindowType.SceneWindow | EWindowType.UserWindow);
    }
}