using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.NetworkVariable;

public class Energy : Stat
{
    protected PlayerMove playerMove;
    protected float runCost = 30f;
    protected float jumpCost = 20f;
    protected float restoringSpeed = 15f;
    protected float baseRestingRestoringBonus = 30f;
    protected float currentRestingRestoringBonus = 30f;

    public void SetPlayerMove(PlayerMove pm)
    {
        playerMove = pm;
        playerMove.OnJumped += Jump;
    }
    private void OnDisable()
    {
        playerMove.OnJumped -= Jump;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsOwner)
        {
            if (playerMove.IsRunning() && !playerMove.IsStandingStill())
            {
                ModifyValue(-runCost * Time.deltaTime);
            }
            else
            {
                ModifyValue((restoringSpeed + (playerMove.IsStandingStill() ? baseRestingRestoringBonus : 0)) * Time.deltaTime);
            }
        }
    }
    protected virtual void Jump()
    {
        ModifyValue(-jumpCost);
    }

    protected virtual void ModifyValue(float value)
    {
        float newValue = Mathf.Clamp(GetValue() + value, 0f, GetMaxValue());
        SetValue(newValue);
        base.OnChange(newValue);
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
