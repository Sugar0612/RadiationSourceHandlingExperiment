
using DG.Tweening;
using Mirror;
using UnityEngine;
using UnityEngine.UI;

public class SceneWindow : WinBase
{
    // 模式一
    Button SceneButton_1;

    // 模式二
    Button SceneButton_2;

    // 模式三
    Button SceneButton_3;

    // 游戏场景
    [Scene]
    public string scene_1;

    public override void Awake()
    {
        base.Awake();
    }

    public override void Start()
    {
        base.Start();

        if (!Config.Get().PicoDevice)
        {
            gameObject.TryFindAndSetStatus("SceneButton_1", true, out SceneButton_1);
            gameObject.TryFindAndSetStatus("SceneButton_2", true, out SceneButton_2);
            gameObject.TryFindAndSetStatus("SceneButton_3", true, out SceneButton_3);

            SceneButton_1.onClick.AddListener(OnClickSceneButton_1);
            SceneButton_2.onClick.AddListener(OnClickSceneButton_2);
            SceneButton_3.onClick.AddListener(OnClickSceneButton_3);
        }
    }

    public void OnClickSceneButton_1()
    {
        GameHelpler.Get().SwitchGameScene(scene_1);
    }

    public void OnClickSceneButton_2()
    {
        GameHelpler.Get().SwitchGameScene(scene_1);
    }
    
    public void OnClickSceneButton_3()
    {
        GameHelpler.Get().SwitchGameScene(scene_1);
    }
}