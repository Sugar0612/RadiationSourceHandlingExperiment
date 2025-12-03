using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
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

    public static GlobalPanel Get()
    {
        if (m_instance == null)
        {
            UnityEngine.Object ob = Resources.Load("Perfabs/UI/Panel/GlobalCanvas");
            if (ob)
            {
                GameObject canvas = GameObject.Instantiate<GameObject>( ob as GameObject);
                m_instance = canvas.GetComponent<GlobalPanel>();
            }
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
        SetActive(false);
        Destroy();
    }

    IEnumerator OnCancelButtonClickedCoroutine(Func<bool> cancelButtonCallback)
    {
        bool cancelRes = cancelButtonCallback.Invoke();
        yield return new WaitUntil(() => cancelRes == true);
        SetActive(false);
        Destroy();
    }

    public void Spawn(string content, Func<bool> okButtonCallback, Func<bool> CancelButtonCallback)
    {
        SetActive(true);
        Init(content, okButtonCallback, CancelButtonCallback);
    }

    public void Destroy()
    {
        SetActive(false);
        UnityEngine.Object.Destroy(this.gameObject);
    }
}
