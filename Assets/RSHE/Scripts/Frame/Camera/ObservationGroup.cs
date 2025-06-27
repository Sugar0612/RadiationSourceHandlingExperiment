using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObservationGroup : MonoBehaviour
{
    static ObservationGroup instance;

    int currIdx = 0;

    List<CameraTag> observerList = new List<CameraTag>() { CameraTag.WitnessFront, CameraTag.WitnessLeftSide, CameraTag.WitnessRightSide };

    public static ObservationGroup Get()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<ObservationGroup>();
        }

        return instance;
    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        //if (obserList.Count > 0)
        //{
        //    SwitchObserver();
        //}
    }

    public void Prev()
    {
        currIdx = (currIdx - 1 + observerList.Count) % observerList.Count;
        SwitchObserver();
    }

    public void Next()
    {
        currIdx = (currIdx + 1) % observerList.Count;
        SwitchObserver();
    }

    void SwitchObserver()
    {
        Log.cinput("yellow", $"SwitchObserver: {currIdx}");
        CameraManager.Get().SwitchCamera(observerList[currIdx]);
    }

    public int GetCurrIdx() => currIdx + 1;

    public int GetObserverCount() => observerList.Count;
}
