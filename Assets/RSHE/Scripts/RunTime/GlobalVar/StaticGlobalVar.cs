
using Mirror;
using UnityEngine;

public static class StaticGlobalVar
{
    public static MyNetworkDiscovery NetworkDiscovery { get { return UnityEngine.Object.FindObjectOfType<MyNetworkDiscovery>(); } }

    public static string playerName = "";

    /// <summary> Join in PlayGame Player Number. </summary>
    public static int PersonCount = -1;

    /// <summary> сно╥дёй╫ </summary>
    public static EGameMode GameMode = EGameMode.None;
}