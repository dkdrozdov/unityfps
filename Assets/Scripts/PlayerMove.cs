using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.NetworkVariable;

public class PlayerMove : NetworkBehaviour
{
    public Energy energy;
    public delegate void JumpAction();
    public event JumpAction OnJumped;
    public CharacterController controller;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    NetworkVariableBool isGrounded = new NetworkVariableBool(new NetworkVariableSettings
    {
        WritePermission = NetworkVariablePermission.ServerOnly,
        ReadPermission = NetworkVariablePermission.Everyone
    });
    NetworkVariableBool isRunning = new NetworkVariableBool(new NetworkVariableSettings
    {
        WritePermission = NetworkVariablePermission.OwnerOnly,
        ReadPermission = NetworkVariablePermission.Everyone
    }, false);
    NetworkVariableBool isStandingStill = new NetworkVariableBool(new NetworkVariableSettings
    {
        WritePermission = NetworkVariablePermission.OwnerOnly,
        ReadPermission = NetworkVariablePermission.Everyone
    }, true);
    NetworkVariableFloat currentSpeed = new NetworkVariableFloat(new NetworkVariableSettings
    {
        WritePermission = NetworkVariablePermission.OwnerOnly,
        ReadPermission = NetworkVariablePermission.Everyone
    });
    public float baseSpeed = 7f;
    public float runBonusSpeed = 6f;
    public float jumpHeight = 4f;
    public float gravity = -29.43f;
    NetworkVariableFloat gravityVelocity = new NetworkVariableFloat(new NetworkVariableSettings
    {
        WritePermission = NetworkVariablePermission.OwnerOnly,
        ReadPermission = NetworkVariablePermission.Everyone
    });

    private void Awake()
    {
        PlayerInitializer playerInitializer = gameObject.GetComponent<PlayerInitializer>();
        groundCheck = playerInitializer.GetGroundCheck();
        controller = playerInitializer.GetCharacterController();
        groundMask = LayerMask.GetMask("Ground");
    }
    // Start is called before the first frame update
    void Start()
    {
        currentSpeed.Value = baseSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsServer)
        {
            isGrounded.Value = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        }
        if (IsOwner)
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            isStandingStill.Value = ((x == 0) && (z == 0) && isGrounded.Value);

            Vector3 move = transform.right * x + transform.forward * z;
            if (Input.GetButtonDown("Run") && energy.AbleToRun())
            {
                isRunning.Value = true;
            }
            if (Input.GetButtonUp("Run") || (!energy.AbleToRun()))
            {
                isRunning.Value = false;
            }

            controller.Move(move * (currentSpeed.Value + (isRunning.Value ? runBonusSpeed : 0)) * Time.deltaTime);

            if (isGrounded.Value && gravityVelocity.Value < 0)
            {
                gravityVelocity.Value = -4f;
            }
            if (!isGrounded.Value)
            {
                gravityVelocity.Value += gravity * Time.deltaTime;
            }
            if (Input.GetButtonDown("Jump") && isGrounded.Value && energy.AbleToJump())
            {
                gravityVelocity.Value = Mathf.Sqrt(jumpHeight * (-2f) * gravity);
                OnJumped?.Invoke();
            }
            controller.Move(Vector3.up * gravityVelocity.Value * Time.deltaTime);
        }
    }
    public bool IsRunning()
    {
        return isRunning.Value;
    }
    public bool IsGrounded()
    {
        return isGrounded.Value;
    }
    public bool IsStandingStill()
    {
        return isStandingStill.Value;
    }
    public void SetEnergy(Energy energyInstance)
    {
        energy = energyInstance;
    }
}
