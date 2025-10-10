using Cysharp.Threading.Tasks;
using System.Collections.Generic;

public class PlayerManager
{
    static PlayerManager _singleton;

    public static PlayerManager Get()
    {
        if (_singleton == null)
            _singleton = new PlayerManager();

        return _singleton;
    }

    public Dictionary<EIdentity, VRNetworkPlayerController> PlayerDic = new Dictionary<EIdentity, VRNetworkPlayerController>();

    public void Register(EIdentity identity, VRNetworkPlayerController player)
    {
        Log.cinput("red", $"@@ PlayerManager Register: {identity.ToString()}");
        if (!PlayerDic.ContainsKey(identity))
        {
            PlayerDic.Add(identity, player);
        }
        else
        {
            PlayerDic[identity] = player;
        }
    }

    public void UnRegister(EIdentity identity)
    {
        if (PlayerDic.ContainsKey(identity))
        {
            PlayerDic.Remove(identity);
        }
    }

    public VRNetworkPlayerController GetPlayer(EIdentity identity)
    {
        if (identity == EIdentity.None) return null;
        return PlayerDic[identity];
    }
}
