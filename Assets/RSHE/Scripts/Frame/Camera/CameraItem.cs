using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraItem : MonoBehaviour
{
    public CameraTag cameraTag;

    public void Awake()
    {
        Log.cinput("yellow", $"{cameraTag.ToString()} Awake!");

        CameraManager.Get().Register(this);
    }

    private void OnDestroy()
    {
        // Log.cinput("yellow", $"{cameraTag.ToString()} OnDestroy!");
        CameraManager.Get().Remove(this);
    }
}
