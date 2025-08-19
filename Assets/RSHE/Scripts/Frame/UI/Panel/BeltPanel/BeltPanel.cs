using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BeltPanel : NetworkBehaviour
{
    #region UI¿Ø¼þ

    public PutItem PutItem_1;

    protected List<RecordItem> _recordList = new List<RecordItem>();

    #endregion

    protected List<float> _valueList = new List<float>();

    virtual public void Start()
    {
        _recordList = GetComponentsInChildren<RecordItem>().ToList();

        for (int i = 0; i < _recordList.Count; ++i)
        {
            int index = i;
            _recordList[i].RecordButton.onClick.AddListener(() =>
            {
                _recordList[index].SetValueText(Utility.Record(gameObject));
            });
        }
    }

    public virtual void RpcOnClickedPutButton()
    {
        foreach (RecordItem item in _recordList)
        {
            if (item.SelectedToggle.isOn)
            {
                _valueList.Add(item.Value);
            }
        }    
        PutItem_1.OnClickedPutButton();
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive<Image>(active);
        gameObject.SetActive<TextMeshProUGUI>(active);
    }
}
