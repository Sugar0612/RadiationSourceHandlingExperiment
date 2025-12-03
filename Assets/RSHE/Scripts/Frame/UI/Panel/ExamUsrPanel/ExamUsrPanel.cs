using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExamUsrPanel : MonoBehaviour
{
    public Button backButton;

    public Image MaskImage;

    public GameObject itemTemplate;

    public Transform parentTransform;
    
    public MistakesPanel MistaskesPanelObject;

    List<ExamUsrItem> examUsrList = new List<ExamUsrItem>();

    ExamRecordPanel _examRecordPanel;

    private void Start()
    {
        SetActive(false);
        backButton.onClick.AddListener(() =>
        {
            Clear();
            SetActive(false);
        });
    }

    public void Init(List<UsrData> list)
    {
        // Log.cinput("green", $"@@@ ExamUsrPanel Init");
        SetActive(true);
        foreach (var data in list)
        {
            ExamUsrItem item = GameObject.Instantiate(itemTemplate, parentTransform).GetComponent<ExamUsrItem>();
            item.Init(data, MistaskesPanelObject);
            item.SetActive(true);
            examUsrList.Add(item);
        }
    }

    public void SetActive(bool active)
    {
        MaskImage.gameObject.SetActive(active);
        gameObject.SetActive(active);
    }

    void Clear()
    {
        for (int i = 0; i < examUsrList.Count; ++i)
        {
            examUsrList[i].SetActive(false);
        }
        examUsrList.Clear();
    }
}
