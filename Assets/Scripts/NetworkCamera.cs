using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

public class NetworkCamera : NetworkBehaviour
{
    Camera playerCamera;
    AudioListener playerListener;
    // Start is called before the first frame update
    void Start()
    {
        playerCamera = GetComponent<Camera>();
        playerListener = GetComponent<AudioListener>();
        if (!IsOwner)
        {
            playerCamera.enabled = false;
            playerListener.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
