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

    void Init(string content, Action okButtonCallback, Action cancelButtonCallback)
    {
        contentText.text = content;
        okButton.onClick.AddListener(() => { okButtonCallback.Invoke(); SetActive(false); });
        cancelButton.onClick.AddListener(() => { cancelButtonCallback.Invoke(); SetActive(false); });
    }

    public void Spawn(string content, Action okButtonCallback, Action CancelButtonCallback)
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
