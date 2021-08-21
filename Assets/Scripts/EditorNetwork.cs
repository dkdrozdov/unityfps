using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
enum StartMode
{
    Client, Host
}
public class EditorNetwork : MonoBehaviour
{
    [SerializeField]
    StartMode mode;
    // Start is called before the first frame update
    void Start()
    {
        if (Application.isEditor)
        {
            NetworkManager netManager = GetComponentInParent<NetworkManager>();
            netManager = GetComponentInParent<NetworkManager>();
            switch (mode)
            {
                case StartMode.Client:
                    netManager.StartClient();
                    break;
                case StartMode.Host:
                    netManager.StartHost();
                    break;
                default: break;
            }
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
