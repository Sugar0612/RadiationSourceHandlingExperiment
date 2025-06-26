
public static class StaticGlobalVar
{
    // 是否是PICO设备
    //public static int isPicoDevice
    //{
    //    get
    //    {
    //        return Config.Get().projectConfig.PicoDevice;
    //    }
    //}

    public static MyNetworkDiscovery networkDiscovery
    {
        get
        {
            return UnityEngine.Object.FindObjectOfType<MyNetworkDiscovery>();
        }
    }
}