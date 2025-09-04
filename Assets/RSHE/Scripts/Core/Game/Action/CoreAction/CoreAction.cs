using Mirror;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System;

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
        Log.cinput("yellow", "HostIssuesTheGoNext");
        if (_instance != null)
        {
            StartCoroutine(TimesUpRun(gamePkg, canGoOn));
        }
    }

    IEnumerator TimesUpRun(GameColliderPackage gamePkg, bool canGoOn)
    {
        if (_instance != null)
        {
            CoreAction.Get()?.SetTaskArrowActive(gamePkg, false);

            yield return new WaitForSeconds(1.0f);

            if (canGoOn)
            {
                Log.cinput("yellow", "In TimesUpRun");
                if (StaticGlobalVar.IsHost)
                {
                    if (!gamePkg.TaskItem.IsAlwayShow)
                    {
                        GameSteps.Get().RunEnd();
                    }
                }
                GameSteps.Get().Next();
            }
        }
    }

    /// <summary> 设置箭头的Active </summary>
    public void SetTaskArrowActive(GameColliderPackage gamePkg, bool active)
    {
        if (gamePkg != null && _instance != null)
        {
            foreach (Arrow arrow in gamePkg.TaskItem.ArrowList)
                arrow.SetActive(active);
        }
    }

    /// <summary> 设置音频状态 </summary>
    public void SetAudioStatus(GameColliderPackage gamePkg, bool active)
    {
        if (gamePkg != null && _instance != null)
        {
            AudioController.Get().Play(gamePkg.TaskItem.HintAudio);
        }
    }

    /// <summary> 用于教学模式自动执行Start Task </summary>
    public void AutoRunStartTask(GameColliderPackage gamePkg, Action timeupAction = null)
    {
        if (gamePkg != null && _instance != null)
        {
            CoreAction.Get().SetTaskArrowActive(gamePkg, true);
            CoreAction.Get().SetAudioStatus(gamePkg, true);
            Timer.Delay(gamePkg.TaskItem.duration,
            () => 
            {
                if (timeupAction != null)
                    timeupAction();
                HostIssuesTheGoNext(gamePkg, true);
            });
        }
    }
}
