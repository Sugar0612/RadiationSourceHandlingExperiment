using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordPanel : NetworkBehaviour
{
    #region UI控件

    public RecordItem PlaceButton_1;

    public RecordItem PlaceButton_2;

    public RecordItem PlaceButton_3;

    public RecordItem PlaceButton;

    #endregion

    #region 场景物体

    /// <summary> 放射源1 </summary>
    Transform _targetTrans_1;

    /// <summary> 放射源2 </summary>
    Transform _targetTrans_2;

    /// <summary> 当前围栏列表的Idx </summary>
    int _currFenceIdx = 0;

    #endregion

    List<float> _disanceList = new List<float>();

    float _disanceMin = 0.0f; 

    void Start()
    {
        PlaceButton_2.SetActive(false);
        PlaceButton_3.SetActive(false);
        PlaceButton.SetActive(false);

        _targetTrans_1 = SceneObjectManager.Get().RadioactiveSource_1;
        _targetTrans_2 = SceneObjectManager.Get().RadioactiveSource_2;
    }

    public void Record()
    {
        _disanceList.Add(_disanceMin);
    }

    public void Update()
    {
        // StartCoroutine(CalculateTheMinDistance());

        if (_targetTrans_1 != null && _targetTrans_2 != null)
        {
            Vector3 xzTargetPos_1 = new Vector3(_targetTrans_1.position.x, 0.0f, _targetTrans_1.position.z);
            Vector3 xzTargetPos_2 = new Vector3(_targetTrans_2.position.x, 0.0f, _targetTrans_2.position.z);
            Vector3 xzThisPos = new Vector3(gameObject.transform.position.x, 0.0f, gameObject.transform.position.z);

            float disance_1 = Vector3.Distance(xzThisPos, xzTargetPos_1);
            float disance_2 = Vector3.Distance(xzThisPos, xzTargetPos_2);
            _disanceMin = Mathf.Min(disance_1, disance_2);
            //Log.cinput("yellow", $"@@@ disance_1：{disance_1}, disance_2: {disance_2}, ShowVal: {DistanceText.text}");
        }
    }

    void OnClickedPlaceButton_1()
    {
        Record();
        PlaceButton_1.OnClickedRecordButton();
        PlaceButton_2.SetActive(true);
    }

    void OnClickedPlaceButton_2()
    {
        Record();
        PlaceButton_2.OnClickedRecordButton();
        PlaceButton_3.SetActive(true);
    }

    void OnClickedPlaceButton_3()
    {
        Record();
        PlaceButton_3.OnClickedRecordButton();
        PlaceButton.SetActive(true);
    }

    void OnClickedPlaceButton()
    {

        foreach (var fence in SceneObjectManager.Get().FencesList)
            fence.SetRendererEnable(true);

        PlaceButton.OnClickedRecordButton();
   
        // TODO..考核判断是否正确
    }
}
