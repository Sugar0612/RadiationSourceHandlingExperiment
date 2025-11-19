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

    public Button ReasonButton;

    public void Init(UsrData data)
    {
        UsrName.text = data.identity.ToString();
        Score.text = data.Score.ToString();
        ReasonButton.onClick.AddListener(ReasonButtonOnClicked);
    }

    void ReasonButtonOnClicked()
    {
        // TODO.
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }
}
