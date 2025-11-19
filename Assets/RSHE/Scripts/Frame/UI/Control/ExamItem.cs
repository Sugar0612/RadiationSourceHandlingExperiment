using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExamItem : MonoBehaviour
{
    public Button examRecordButton;

    public Button DeleteButton;

    public ExamRecordPanel ExamRecordPanelObject;

    string _examTime = "";

    public void Init(string examTime, ExamRecordPanel parentPanel)
    {
        SetActive(true);
        examRecordButton.GetComponentInChildren<TextMeshProUGUI>().text = examTime;
        DeleteButton.onClick.AddListener(OnClickedDeleteButton);
        examRecordButton.onClick.AddListener(OnClickedExamRecordButton);
        ExamRecordPanelObject = parentPanel;
        _examTime = examTime;
    }

    void OnClickedDeleteButton()
    {
        GlobalPanel.Get().Spawn("是否删除本次考试记录？", DeleteThisExamData, CancelDelete);
    }

    void OnClickedExamRecordButton()
    {
        Log.cinput("green", "@@@ OnClickedExamRecordButton");
        ExamRecordPanelObject.ShowExamUsrPanel(_examTime);
    }

    void DeleteThisExamData()
    {
        if (!Scorer.Get().isSpawned)
        {
            Scorer.Get().Spawn();
        }

        string examTime = examRecordButton.GetComponentInChildren<TextMeshProUGUI>().text;
        Scorer.Get().DeleteItem(examTime);
        SetActive(false);
        Destroy(gameObject);
    }

    void CancelDelete()
    {
        GlobalPanel.Get().Destroy();
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }
}
