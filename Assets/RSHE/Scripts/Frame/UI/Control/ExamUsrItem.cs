using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ExamUsrItem : MonoBehaviour
{
    public TextMeshProUGUI UsrName;

    public TextMeshProUGUI Score;

    MistakesPanel _mistakesPanel;

    public Button ReasonButton;

    UsrData _usrData;

    public void Init(UsrData data, MistakesPanel mistakesPanel)
    {
        _usrData = data;
        _mistakesPanel = mistakesPanel;
        UsrName.text = data.identity.ToString();
        Score.text = data.Score.ToString();
        ReasonButton.onClick.AddListener(ReasonButtonOnClicked);
    }

    void ReasonButtonOnClicked()
    {
        if (_usrData != null)
        {
            _mistakesPanel.Init(_usrData.IncorrectList);
        }
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }
}
