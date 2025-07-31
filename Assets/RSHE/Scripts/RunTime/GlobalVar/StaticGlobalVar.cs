
using Mirror;
using UnityEngine;

public static class StaticGlobalVar
{
    public static MyNetworkDiscovery NetworkDiscovery { get { return UnityEngine.Object.FindObjectOfType<MyNetworkDiscovery>(); } }

    public static string playerName = "";

    /// <summary> Join in PlayGame Player Number. </summary>
    public static int PersonCount = -1;

    /// <summary> 游戏模式 </summary>
    public static EGameMode GameMode = EGameMode.None;

    /// <summary> 是否为主机模式（服务器和客户端都在一台设备上） </summary>
    public static bool IsHost { get => NetworkServer.active && NetworkClient.active; }

    /// <summary> 本设备是服务器 </summary>
    public static bool IsServer { get => NetworkServer.active; }

    /// <summary> 本设备的客户端 </summary>
    public static bool IsClient { get => NetworkClient.active; }
}