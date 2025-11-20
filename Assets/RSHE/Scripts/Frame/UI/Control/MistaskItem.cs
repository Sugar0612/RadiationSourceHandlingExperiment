using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MistaskItem : MonoBehaviour
{
    public TextMeshProUGUI MistaskText;

    public TextMeshProUGUI ScoreText;

    IncorrectData _incorrData;

    public void Init(IncorrectData data)
    {
        _incorrData = data;
        MistaskText.text = _incorrData.ExamMistake;
        ScoreText.text = _incorrData.Scores.ToString("F2");
        SetActive(true);
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }
}
