using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.NetworkVariable;

public class Energy : NetworkBehaviour
{
    protected PlayerMove playerMove;
    protected float maxEnergy = 100f;
    protected float currentEnergy = 100f;
    protected float runCost = 30f;
    protected float jumpCost = 20f;
    protected float restoringSpeed = 15f;
    protected float baseRestingRestoringBonus = 30f;
    protected float currentRestingRestoringBonus = 30f;

    protected virtual void Awake()
    {
        PlayerInitializer playerInitializer = gameObject.GetComponent<PlayerInitializer>();
        playerMove = playerInitializer.GetPlayerMove();
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
        currentEnergy += value;
        currentEnergy = Mathf.Clamp(currentEnergy, 0f, maxEnergy);
    }

    //  Read methods

    public float GetValue()
    {
        return currentEnergy;
    }

    public bool AbleToJump()
    {
        return currentEnergy - jumpCost >= 0;
    }
    public bool AbleToRun()
    {
        return currentEnergy - runCost * Time.deltaTime >= 0;
    }
}
