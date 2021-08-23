using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.NetworkVariable;
using MLAPI.Messaging;

public class PlayerMove : NetworkBehaviour
{
    Energy energy;
    CharacterController controller;
    [SerializeField] Transform groundCheck;
    LayerMask groundMask;
    public delegate void JumpAction();
    public event JumpAction OnJumped;
    float groundDistance = 0.4f;
    [SerializeField]
    NetworkVariableBool isGrounded = new NetworkVariableBool(new NetworkVariableSettings
    {
        WritePermission = NetworkVariablePermission.OwnerOnly,
        ReadPermission = NetworkVariablePermission.Everyone
    });
    [SerializeField]
    NetworkVariableBool isRunning = new NetworkVariableBool(new NetworkVariableSettings
    {
        WritePermission = NetworkVariablePermission.OwnerOnly,
        ReadPermission = NetworkVariablePermission.Everyone
    }, false);
    [SerializeField]
    NetworkVariableBool isStandingStill = new NetworkVariableBool(new NetworkVariableSettings
    {
        WritePermission = NetworkVariablePermission.OwnerOnly,
        ReadPermission = NetworkVariablePermission.Everyone
    }, true);
    [SerializeField]
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

    public void SetGroundCheck(Transform gc)
    {
        groundCheck = gc;
    }
    public void SetController(CharacterController c)
    {
        controller = c;
    }
    void Start()
    {
        currentSpeed.Value = baseSpeed;
        groundMask = LayerMask.GetMask("Ground");
    }

    void Update()
    {

        if (IsOwner)
        {
            SetIsGrounded(Physics.CheckSphere(groundCheck.position, groundDistance, groundMask));
            SetCurrentSpeed(baseSpeed + (IsRunning() ? runBonusSpeed : 0));
            if (IsGrounded() && GetGravityVelocity() < 0)
            {
                SetGravityVelocity(-4f);
            }
            if (!IsGrounded())
            {
                SetGravityVelocity(GetGravityVelocity() + gravity * Time.deltaTime);
            }
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            SetIsStandingStill(((x == 0) && (z == 0) && IsGrounded()));
            if (Input.GetButtonDown("Run") && energy.AbleToRun())
            {
                SetIsRunning(true);
            }
            if (Input.GetButtonUp("Run") || (!energy.AbleToRun()))
            {
                SetIsRunning(false);
            }
            Vector3 move = transform.right * x + transform.forward * z;
            controller.Move(move * GetCurrentSpeed() * Time.deltaTime);
            if (Input.GetButtonDown("Jump"))
            {
                if (IsGrounded() && energy.AbleToJump())
                {
                    gravityVelocity.Value = Mathf.Sqrt(jumpHeight * (-2f) * gravity);
                    OnJumped?.Invoke();
                }
            }
            controller.Move(Vector3.up * GetGravityVelocity() * Time.deltaTime);
        }
    }

    public float GetGravityVelocity()
    {
        return gravityVelocity.Value;
    }
    public void SetGravityVelocity(float newValue)
    {
        if (IsOwner)
        {
            gravityVelocity.Value = newValue;
        }
    }
    public float GetCurrentSpeed()
    {
        return currentSpeed.Value;
    }
    public void SetCurrentSpeed(float newValue)
    {
        if (IsOwner)
        {
            currentSpeed.Value = newValue;
        }
        else
        {
            Debug.LogErrorFormat("SetCurrentSpeed called on client! PlayerMove {0}", this);
        }
    }
    public bool IsGrounded()
    {
        return isGrounded.Value;
    }
    public void SetIsGrounded(bool newValue)
    {
        if (IsOwner)
        {
            isGrounded.Value = newValue;
        }
    }
    public bool IsRunning()
    {
        return isRunning.Value;
    }
    public void SetIsRunning(bool newValue)
    {
        isRunning.Value = newValue;
    }
    public bool IsStandingStill()
    {
        return isStandingStill.Value;
    }
    public void SetIsStandingStill(bool newValue)
    {
        isStandingStill.Value = newValue;
    }
    public void SetEnergy(Energy energyInstance)
    {
        energy = energyInstance;
    }
}
