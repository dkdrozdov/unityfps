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
    protected virtual void Awake()
    {
        PlayerInitializer playerInitializer = gameObject.GetComponent<PlayerInitializer>();
        playerMove = playerInitializer.GetPlayerMove();
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
        currentValue.Value += value;
        currentValue.Value = Mathf.Clamp(currentValue.Value, 0f, maxValue);
        base.OnChange(currentValue.Value);
    }

    //  Read methods
    public bool AbleToJump()
    {
        return currentValue.Value - jumpCost >= 0;
    }
    public bool AbleToRun()
    {
        return currentValue.Value - runCost * Time.deltaTime >= 0;
    }
}
