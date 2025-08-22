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

    private List<RadiationSource> _radiationSourceList = new List<RadiationSource>();
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
                _recordList[index].SetValueText(ScanArea());
            });
        }
    }

    private void Update()
    {

    }

    float ScanArea()
    {
        _radiationSourceList.Clear();

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, ScanRadius);

        foreach (Collider col in hitColliders)
        {
            RadiationSource rs = col.gameObject.GetComponent<RadiationSource>();
            if (rs)
            {
                if (!rs.IsClear)
                {
                    _radiationSourceList.Add(rs);
                }
            }
        }

        float value = 0.0f;
        foreach (RadiationSource rs in _radiationSourceList)
        {
            value = Math.Max(value, Utility.Record(rs, gameObject));
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

    [Command(requiresAuthority = false)]
    public void CmdOnClickedPutButton(List<GameObject> goList)
    {
        RpcClickedPutButton(goList);
    }

    [ClientRpc]
    public void RpcClickedPutButton(List<GameObject> goList)
    {
        PutItem_1.OnClickedPutButton();

        foreach (var go in goList)
            go.SetActive<Renderer>(true);

        // GO on Task...
        
    }
}
