using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLuncher : MonoBehaviour
{
    private void Awake()
    {
        Log.cinput("purple", "================= CameraLuncher Awake.");
        CameraManager.Get().Init();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
