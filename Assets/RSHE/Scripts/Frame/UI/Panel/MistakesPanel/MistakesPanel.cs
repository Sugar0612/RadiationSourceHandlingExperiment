using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MistakesPanel : MonoBehaviour
{
    [SerializeField]
    GameObject MistakeItemTemplate;

    [SerializeField]
    Transform itemParent;

    [SerializeField]
    Button closeButton;

    [SerializeField]
    TextMeshProUGUI emptyText;

    [SerializeField]
    GameObject propertyObj;

    List<MistaskItem> _mistaskesList = new List<MistaskItem>();

    private void Start()
    {
        SetActive(false);
        closeButton.onClick.AddListener(() =>
        {
            SetActive(false);
            Destroy();
        });
    }

    public void Init(List<IncorrectData> list)
    {
        emptyText.gameObject.SetActive(list.Count == 0);
        propertyObj.gameObject.SetActive(list.Count != 0);

        Destroy();
        foreach (IncorrectData data in list)
        {
            MistaskItem item = GameObject.Instantiate(MistakeItemTemplate, itemParent).GetComponent<MistaskItem>();
            item.Init(data);
            _mistaskesList.Add(item);
        }
        SetActive(true);
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
