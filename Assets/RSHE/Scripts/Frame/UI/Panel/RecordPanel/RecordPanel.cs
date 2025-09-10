using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecordPanel : NetworkBehaviour
{
    #region UI¿Ø¼þ

    public PutItem PutItem_1;

    protected List<RecordItem> _recordList = new List<RecordItem>();

    #endregion

    #region ¼ì²â²ÎÊý
    public float ScanRadius = 1.5f; // É¨Ãè°ë¾¶

    public float scanInterval = 1f; // É¨Ãè¼ä¸ô£¨Ãë£©

    private float _timer;

    private List<Detector> _detctorList = new List<Detector>();

    public GameObject TestPoint;

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
                // _recordList[index].SetValueText(ScanArea());
                CmdRecordButtonClicked(index);
            });
        }
    }

    private void Update()
    {

    }

    float ScanArea()
    {
        _detctorList.Clear();

        Collider[] hitColliders = Physics.OverlapSphere(TestPoint.transform.position, ScanRadius);

        foreach (Collider col in hitColliders)
        {
            Detector detector = col.gameObject.GetComponentInChildren<Detector>();
            if (detector)
            {
                _detctorList.Add(detector);
            }
        }

        float value = 0.0f;
        foreach (Detector detctor in _detctorList)
        {
            float temp = detctor.DeviceValue;
            value = Math.Max(value, temp);
        }
        return value;
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

    #region Command Function
    [Command(requiresAuthority = false)]
    public void CmdOnClickedPutButton(List<GameObject> goList)
    {
        RpcClickedPutButton(goList);
    }

    [Command(requiresAuthority = false)]
    void CmdRecordButtonClicked(int i)
    {
        RpcRecordButtonClicked(i);
    }
    #endregion

    #region Client RPC Function
    [ClientRpc]
    public void RpcClickedPutButton(List<GameObject> goList)
    {
        PutItem_1.OnClickedPutButton();

        foreach (var go in goList)
            go.SetActive<Renderer>(true);

        // GO on Task... 
    }

    [ClientRpc]
    void RpcRecordButtonClicked(int i)
    {
        _recordList[i].SetValueText(ScanArea());
        _recordList[i].OnClickedRecordButton();
    }
    #endregion
}
