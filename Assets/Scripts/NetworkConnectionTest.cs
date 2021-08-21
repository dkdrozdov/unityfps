using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Messaging;

public class NetworkConnectionTest : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (IsClient)
        {
            Debug.Log("Client connected!");
        }
        if (IsHost)
        {
            Debug.Log("Host connected!");
        }
        if (IsLocalPlayer)
        {
            Debug.Log("Local player connected!");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
