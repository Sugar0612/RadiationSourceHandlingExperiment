using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCheckDispenser : MonoBehaviour
{
    /// <summary> baseAction 多态委托 </summary>
    private delegate CheckBase CheckCreator();

    /// <summary> 不同的EGameMode，存储不同的action委托 </summary>
    private static readonly Dictionary<EGameMode, CheckCreator> s_checkCreators;

    static GameCheckDispenser _instance = null;

    public static GameCheckDispenser Get()
    {
        if (_instance == null)
        {
            _instance = new GameCheckDispenser();
        }
        return _instance;
    }

    static GameCheckDispenser()
    {
        s_checkCreators = new Dictionary<EGameMode, CheckCreator>
        {
            { EGameMode.Teaching, () => GameObject.FindObjectOfType<TeachingCheck>() },
            { EGameMode.PracticalTraining, () => GameObject.FindObjectOfType<PracticalTrainingCheck>() },
            { EGameMode.SelfTest, () => GameObject.FindObjectOfType<SelfTestCheck>() },
            { EGameMode.Assessment, () => GameObject.FindObjectOfType<AssessmentCheck>() }
        };
    }

    /// <summary>
    /// 分发器
    /// </summary>
    public CheckBase Dispenser(EGameMode mode)
    {
        if (s_checkCreators.TryGetValue(mode, out var creator))
        {
            return creator();
        }
        return null;
    }
}
