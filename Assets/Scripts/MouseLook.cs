using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Messaging;
using MLAPI.NetworkVariable;
using System;

public class MouseLook : NetworkBehaviour
{
    public float mouseSensitivity = 100f;
    float verticalRotation = 0f;
    Transform playerParent;

    // Start is called before the first frame update
    void Start()
    {
        playerParent = transform.root.gameObject.transform;
        if (IsOwner)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (IsOwner)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            verticalRotation -= mouseY;
            verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
            playerParent.Rotate(Vector3.up * mouseX);
        }
    }
}