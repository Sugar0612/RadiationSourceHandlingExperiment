using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FlagPanel : MonoBehaviour
{
    #region UI控件

    public RecordItem RecordButton_1;

    public RecordItem RecordButton_2;

    public RecordItem PlaceButton_1;

    public RecordItem RecordButton_3;

    public RecordItem RecordButton_4;

    public RecordItem PlaceButton_2;

    #endregion

    float _disanceMin = 0.0f;

    List<float> _disanceList = new List<float>();

    private void Start()
    {
        RecordButton_1.RecordButton.onClick.AddListener(() => OnClickedRecordButton_1());
        RecordButton_2.RecordButton.onClick.AddListener(() => OnClickedRecordButton_2());
        PlaceButton_1.RecordButton.onClick.AddListener(() => OnClickedPlaceButton_1());

        RecordButton_3.RecordButton.onClick.AddListener(() => OnClickedRecordButton_3());
        RecordButton_4.RecordButton.onClick.AddListener(() => OnClickedRecordButton_4());
        PlaceButton_2.RecordButton.onClick.AddListener(() => OnClickedPlaceButton_2());

        RecordButton_2.SetActive(false);
        PlaceButton_1.SetActive(false);
        RecordButton_3.SetActive(false);
        RecordButton_4.SetActive(false);
        PlaceButton_2.SetActive(false);
    }

    private void Update()
    {
        _disanceMin = Utility.Record(gameObject);
    }

    public void Record()
    {
        _disanceList.Add(_disanceMin);
    }

    void OnClickedRecordButton_1()
    {
        Record();
        RecordButton_1.OnClickedRecordButton();
        RecordButton_2.SetActiveForButton(true);
    }

    void OnClickedRecordButton_2()
    {
        Record();
        RecordButton_2.OnClickedRecordButton();
        PlaceButton_1.SetActiveForButton(true);
    }
    void OnClickedPlaceButton_1()
    {
        SceneObjectManager.Get().CmdSetSceneObjectActive(SceneObjectManager.Get().FlagList[0], true);

        PlaceButton_1.OnClickedRecordButton();
        RecordButton_3.SetActiveForButton(true);
        // TODO..考核判断是否正确
    }

    void OnClickedRecordButton_3()
    {
        Record();
        RecordButton_3.OnClickedRecordButton();
        RecordButton_4.SetActiveForButton(true);
    }

    void OnClickedRecordButton_4()
    {
        Record();
        RecordButton_4.OnClickedRecordButton();
        PlaceButton_2.SetActiveForButton(true);
    }
    void OnClickedPlaceButton_2()
    {
        SceneObjectManager.Get().CmdSetSceneObjectActive(SceneObjectManager.Get().FlagList[1], true);

        PlaceButton_2.OnClickedRecordButton();
        GameSteps.Get().Next();
        // TODO..考核判断是否正确
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive<Image>(active);
        gameObject.SetActive<TextMeshProUGUI>(active);
    }
}
