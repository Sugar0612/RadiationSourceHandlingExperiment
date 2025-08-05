using Mirror;
using UnityEngine.InputSystem;


public partial class CoreAction : NetworkBehaviour
{
    static CoreAction _instance;

    public static CoreAction Get()
    {
        if (_instance == null)
        {
            _instance = FindObjectOfType<CoreAction>();
        }

        return _instance;
    }

    public void HostIssuesTheGoNext(GameColliderPackage gamePkg, bool canGoOn)
    {
        if (canGoOn)
        {
            if (StaticGlobalVar.IsHost)
                gamePkg.TaskItem.GoEndTaskEvent();

            GameSteps.Get().Next();
        }
    }  
}
