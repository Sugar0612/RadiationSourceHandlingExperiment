using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BeltPanel : NetworkBehaviour
{
    #region UI控件

    public RecordItem PlaceButton_1;

    public RecordItem PlaceButton_2;

    public RecordItem PlaceButton_3;

    public RecordItem PlaceButton;

    #endregion

    List<float> _disanceList = new List<float>();

    float _disanceMin = 0.0f; 

    void Start()
    {
        PlaceButton_1.RecordButton.onClick.AddListener(() => OnClickedPlaceButton_1());
        PlaceButton_2.RecordButton.onClick.AddListener(() => OnClickedPlaceButton_2());
        PlaceButton_3.RecordButton.onClick.AddListener(() => OnClickedPlaceButton_3());
        PlaceButton.RecordButton.onClick.AddListener(() => OnClickedPlaceButton());

        PlaceButton_2.SetActive(false);
        PlaceButton_3.SetActive(false);
        PlaceButton.SetActive(false);
    }

    public void Record()
    {
        _disanceList.Add(_disanceMin);
    }

    public void Update()
    {
        // StartCoroutine(CalculateTheMinDistance());

        _disanceMin = Utility.Record(gameObject);
    }

    void OnClickedPlaceButton_1()
    {
        Record();
        PlaceButton_1.OnClickedRecordButton();
        PlaceButton_2.SetActiveForButton(true);
    }

    void OnClickedPlaceButton_2()
    {
        Record();
        PlaceButton_2.OnClickedRecordButton();
        PlaceButton_3.SetActiveForButton(true);
    }

    void OnClickedPlaceButton_3()
    {
        Record();
        PlaceButton_3.OnClickedRecordButton();
        PlaceButton.SetActiveForButton(true);
    }

    void OnClickedPlaceButton()
    {

        foreach (var fence in SceneObjectManager.Get().FencesList)
            SceneObjectManager.Get().CmdSetSceneObjectActive(fence, true);

        PlaceButton.OnClickedRecordButton();
        CoreAction.Get().EndAction_2(null);

        // TODO..考核判断是否正确
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive<Image>(active);
        gameObject.SetActive<TextMeshProUGUI>(active);
    }
}
