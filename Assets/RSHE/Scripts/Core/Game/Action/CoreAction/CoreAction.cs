using Mirror;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine;
using Cysharp.Threading.Tasks;

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
        SetTaskArrowActive(gamePkg, false);
        if (canGoOn)
        {
            if (StaticGlobalVar.IsHost)
            {
                if (!gamePkg.TaskItem.IsAlwayShow)
                {
                    gamePkg.TaskItem.GoEndTaskEvent();
                }
            }
            GameSteps.Get().Next();
        }
    }

    public void SetTaskArrowActive(GameColliderPackage gamePkg, bool active)
    {
        if (gamePkg != null)
        {
            Log.cinput("yellow", $"ArrowActive is {active}");
            Arrow arrow = gamePkg.TaskItem.Arrow;
            arrow.SetActive(active);
        }
    }
}
