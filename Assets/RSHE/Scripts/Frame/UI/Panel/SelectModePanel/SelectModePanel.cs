using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// 该类用于情景题要场景，游戏模式选择
/// </summary>
public class SelectModePanel : MonoBehaviour
{
    public Button Teacher;

    public Button PracticalTraining;

    public Button SelfTest;

    public Button Assessment;

    private void Start()
    {
        Teacher.onClick.AddListener(() => OnClickedModeButton(EGameMode.Teaching));
        PracticalTraining.onClick.AddListener(() => OnClickedModeButton(EGameMode.PracticalTraining));
        SelfTest.onClick.AddListener(() => OnClickedModeButton(EGameMode.SelfTest));
        Assessment.onClick.AddListener(() => OnClickedModeButton(EGameMode.Assessment));

        SetActive(false);
    }

    public void OnClickedModeButton(EGameMode mode)
    {
        if (NetworkServer.active)
        {
            Game game = FindObjectOfType<Game>();
            game.CmdChangeGameScene(mode);
        }
        //StaticGlobalVar.GameMode = mode;
        //GameHelpler.Get().SwitchGameScene(GameScene);
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive<Image>(active);
        gameObject.SetActive<Button>(active);
        gameObject.SetActive<TextMeshProUGUI>(active);
    }
}
