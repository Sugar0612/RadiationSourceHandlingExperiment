using UnityEngine;
using Mirror;

public class VRJoinWindow : WinBase
{
    public override void Awake()
    {
        base.Awake();
    }

    public override void Start()
    {
        base.Start();

        if (StaticGlobalVar.isPicoDevice == 1)
            OnClickJoinButton();
    }

    public void OnClickJoinButton()
    {
        Log.cinput("green", "Join Button Clicked");
        StartCoroutine(StaticGlobalVar.networkDiscovery.IEStartDiscovery()); //开始查找主机
    }
}