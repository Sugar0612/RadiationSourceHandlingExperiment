using Mirror;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class TestTaskCollider : NetworkBehaviour
{
    [SyncVar]
    public bool IsDetector = false;

    [SyncVar]
    public bool IsPoll = false;

    [SyncVar]
    public bool isUsed = false;

    /// <summary> 两个道具都触发后，等待多久去Goon next task. </summary>
    public float WaitDuration = 3.5f;

    TaskName[] _testTaskArray = new TaskName[3] { TaskName.T6, TaskName.T9, TaskName.T11 };

    Detector _detector;

    PollutionDetector _pollutionDetector;

    TaskInspector _inspector;

    private void Start()
    {
        _inspector = new TaskInspector();
    }

    [ServerCallback]
    public void OnTriggerEnter(Collider other)
    {
        if (_detector == null)
        {
            _detector = other.GetComponentInParent<Detector>();
            if (_detector == null) _detector = other.GetComponentInChildren<Detector>();
            IsDetector = _detector != null;
        }

        if (_pollutionDetector == null)
        {
            _pollutionDetector = other.GetComponentInParent<PollutionDetector>();
            if (_pollutionDetector == null) _pollutionDetector = other.GetComponentInChildren<PollutionDetector>();
            IsPoll = _pollutionDetector != null;
        }

        if (_inspector.InspectionSteps(other, _testTaskArray) && IsPoll && IsDetector && !isUsed)
        {
            // Log.cinput("red", "@@ TestTaskCollider OnTriggerEnter");
            StartCoroutine(GoOnTask());
            _detector?.RpcInvalidateTargetPorpCollider();
            _pollutionDetector?.RpcInvalidateTargetPorpCollider();
        }
    }

    IEnumerator GoOnTask()
    {
        isUsed = true;
        IsPoll = false;
        IsDetector = false;

        TaskName targetTaskName = TaskName.T13;
        foreach (TaskName task in _testTaskArray)
        {
            if (!GameSteps.Get().IsCheckTaskFinished(task))
            {
                targetTaskName = task;
                break;
            }
        }

        yield return new WaitForSeconds(WaitDuration);

        // Utility.DestroyNetworkObject(_detector.gameObject);
        // Utility.DestroyNetworkObject(_pollutionDetector.gameObject);
        GameSteps.Get().CheckTaskGoRun(targetTaskName);
    }

    [ServerCallback]
    public void OnTriggerExit(Collider other)
    {

    }
}
