using Mirror;
using Unity.VisualScripting;
using UnityEngine;

public class TaskCollider_6 : NetworkBehaviour
{
    [SyncVar]
    public bool IsDetector = false;

    [SyncVar]
    public bool IsPoll = false;

    [SyncVar]
    public bool isUsed = false;

    [ServerCallback]
    public void OnTriggerEnter(Collider other)
    {
        Detector detector = other.GetComponentInParent<Detector>();
        PollutionDetector pullDetector = other.GetComponentInParent<PollutionDetector>();

        if (pullDetector)
            IsPoll = true;

        if (detector)
            IsDetector = true;

        if (IsPoll && IsDetector && !isUsed)
        {
            isUsed = true;
            IsPoll = false;
            IsDetector = false;
            GameSteps.Get().CheckTaskGoRun(TaskName.T6);
        }
    }

    public void OnTriggerExit(Collider other)
    {

    }
}