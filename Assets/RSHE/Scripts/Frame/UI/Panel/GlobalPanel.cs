using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class GlobalPanel : MonoBehaviour
{
    #region UI Compents

    public TextMeshProUGUI contentText;

    public Button okButton;

    public Button cancelButton;

    public Image backgroundImg;

    #endregion

    private static GlobalPanel m_instance;
    private static AsyncOperationHandle<GameObject> _loadHandle;
    private static bool _loading;

    /// <summary> 应用启动后预热,避免首次弹出确认框时等待异步加载 </summary>
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void Preload() => Get();

    public static GlobalPanel Get()
    {
        // m_instance 可能是已销毁的假 null,Unity 重载的 == 会正确判定
        if (m_instance == null && !_loading)
        {
            _loading = true;
            _loadHandle = Addressables.InstantiateAsync("UI/GlobalCanvas");
            _loadHandle.Completed += op =>
            {
                _loading = false;
                if (op.Status == AsyncOperationStatus.Succeeded)
                {
                    m_instance = op.Result.GetComponent<GlobalPanel>();

                    // 预热实例化后先隐藏全部控件,等 Spawn 弹窗时再显示
                    if (m_instance != null)
                        m_instance.SetActive(false);
                }
                else
                {
                    Log.cinput("red", $"[Addressables] GlobalCanvas 加载失败: {op.OperationException}");
                }
            };
        }
        return m_instance;
    }

    public void Start()
    {
        m_instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void SetActive(bool active)
    {
        // gameObject.SetActive(active);
        contentText.SetAciveForTheUIControl<TextMeshProUGUI>(active);
        backgroundImg.SetAciveForTheUIControl<Image>(active);
        okButton.SetButtonActive(active);
        cancelButton.SetButtonActive(active);
    }

    void Init(string content, Func<bool> okButtonCallback, Func<bool> cancelButtonCallback)
    {
        contentText.text = content;
        okButton.onClick.AddListener(() => { StartCoroutine(OnOkButtonClickedCoroutine(okButtonCallback)); });
        cancelButton.onClick.AddListener(() => { StartCoroutine(OnCancelButtonClickedCoroutine(cancelButtonCallback)); });
    }

    IEnumerator OnOkButtonClickedCoroutine(Func<bool> okButtonCallback)
    {
        bool okRes = okButtonCallback.Invoke();
        yield return new WaitUntil(() => okRes == true);
        //SetActive(false);
        DestroySelf();
    }

    IEnumerator OnCancelButtonClickedCoroutine(Func<bool> cancelButtonCallback)
    {
        bool cancelRes = cancelButtonCallback.Invoke();
        yield return new WaitUntil(() => cancelRes == true);
        //SetActive(false);
        DestroySelf();
    }

    public void Spawn(string content, Func<bool> okButtonCallback, Func<bool> CancelButtonCallback)
    {
        SetActive(true);
        Init(content, okButtonCallback, CancelButtonCallback);
    }

    public void DestroySelf()
    {
        SetActive(false);

        // 实例来自 Addressables.InstantiateAsync,用 ReleaseInstance 同时销毁实例并释放引用计数
        if (_loadHandle.IsValid())
            Addressables.ReleaseInstance(_loadHandle);
        else
            UnityEngine.Object.Destroy(this.gameObject);
    }
}
