using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MistakesPanel : MonoBehaviour
{
    public GameObject MistakeItemTemplate;

    public Transform itemParent;

    public Button closeButton;

    List<MistaskItem> _mistaskesList = new List<MistaskItem>();

    private void Start()
    {
        closeButton.onClick.AddListener(() =>
        {
            SetActive(false);
            Destroy();
        });
    }

    public void Init(List<IncorrectData> list)
    {
        SetActive(true);
        foreach (IncorrectData data in list)
        {
            MistaskItem item = GameObject.Instantiate(MistakeItemTemplate, itemParent).GetComponent<MistaskItem>();
            item.Init(data);
            _mistaskesList.Add(item);
        }
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    public void Destroy()
    {
        for (int i = 0; i < _mistaskesList.Count; ++i)
        {
            _mistaskesList[i].SetActive(false);
        }
        _mistaskesList.Clear();
    }
}
