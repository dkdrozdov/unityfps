using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.NetworkVariable;
using MLAPI.Messaging;

public class Energy : Stat
{
    [SerializeField] protected PlayerMove playerMove;
    [SerializeField] protected float runCost = 30f;
    [SerializeField] protected float jumpCost = 15f;
    [SerializeField] protected float restoringSpeed = 15f;
    [SerializeField] protected float restingRestoringBonus = 30f;

    public void SetPlayerMove(PlayerMove pm)
    {
        playerMove = pm;
        playerMove.OnJumped += Jump;
    }
    private void OnDisable()
    {
        playerMove.OnJumped -= Jump;
    }

    private void Update()
    {
        if (IsOwner)
        {
            if (playerMove.IsRunning() && !playerMove.IsStandingStill())
            {
                ModifyValue(-runCost * Time.deltaTime);
            }
            else
            {
                ModifyValue((restoringSpeed + (playerMove.IsStandingStill() ? restingRestoringBonus : 0)) * Time.deltaTime);
            }
        }
    }

    protected virtual void Jump()
    {
        ModifyValue(-jumpCost);
    }

    protected virtual void ModifyValue(float value)
    {
        if (IsOwner)
        {
            float newValue = Mathf.Clamp(GetValue() + value, 0f, GetMaxValue());
            SetValue(newValue);
            base.OnChange(newValue);
        }
    }
    //  Read methods
    public bool AbleToJump()
    {
        return GetValue() - jumpCost >= 0;
    }
    public bool AbleToRun()
    {
        return GetValue() - runCost * Time.deltaTime >= 0;
    }
}
