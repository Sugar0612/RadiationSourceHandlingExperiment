
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

    public override void Awake()
    {
        base.Awake();
    }

    public override void Start()
    {
        base.Start();
        
        vrPanel.SetActive(StaticGlobalVar.isPicoDevice == 1);
        normalPanel.SetActive(StaticGlobalVar.isPicoDevice == 0);

        if (StaticGlobalVar.isPicoDevice == 0)
        {
            gameObject.TryFindAndSetStatus("HostButton", true, out hostButton);
            //OnClickHostButton();
        }
    }

    public void OnClickHostButton()
    {
        Log.cinput("green", "Host Button Clicked");
        NetworkManager.singleton.StartHost();
        StaticGlobalVar.networkDiscovery.AdvertiseServer();

        UIController.Get().ShowWindows(EWindowType.SceneWindow | EWindowType.UserWindow);
    }
}