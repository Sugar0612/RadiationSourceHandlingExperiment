using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeDispenser
{
    /// <summary> baseAction 多态委托 </summary>
    private delegate ActionBase ActionCreator();

    /// <summary> 不同的EGameMode，存储不同的action委托 </summary>
    private static readonly Dictionary<EGameMode, ActionCreator> s_actionCreators;

    static GameModeDispenser _instance = null;

    public static GameModeDispenser Get()
    {
        if (_instance == null)
        {
            _instance = new GameModeDispenser();
        }
        return _instance;
    }

    static GameModeDispenser()
    {
        s_actionCreators = new Dictionary<EGameMode, ActionCreator>
        {
            { EGameMode.Teaching, () => GameObject.FindObjectOfType<TeachingAction>() },
            { EGameMode.PracticalTraining, () => GameObject.FindObjectOfType<PracticalTrainingAction>() },
            { EGameMode.SelfTest, () => GameObject.FindObjectOfType<SelfTestAction>() },
            { EGameMode.Assessment, () => GameObject.FindObjectOfType<AssessmentAction>() }
        };
    }

    /// <summary>
    /// 分发器
    /// </summary>
    public ActionBase Dispenser(EGameMode mode)
    {
        if (s_actionCreators.TryGetValue(mode, out var creator))
        {
            return creator();
        }
        return null;
    }
}
