using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.NetworkVariable;

public class PlayerEnergy : Energy
{
    public StatBar energyBar;
    protected override void Awake()
    {
        PlayerInitializer playerInitializer = gameObject.GetComponent<PlayerInitializer>();
        playerMove = playerInitializer.GetPlayerMove();
        energyBar = playerInitializer.GetUI().GetEnergyBar();
    }
    private void OnEnable()
    {
        playerMove.OnJumped += Jump;
    }
    private void OnDisable()
    {
        playerMove.OnJumped -= Jump;
    }
    void Start()
    {
        if (IsOwner)
        {
            energyBar.SetMaxValue(maxEnergy);
        }
    }
    protected override void ModifyValue(float value)
    {
        if (IsOwner)
        {
            base.ModifyValue(value);
            energyBar.SetValue(currentEnergy);
        }
        else
        {
            Debug.LogError("Called PlayerEnergy.ModifyValue() on non-owner application!", this);
        }
    }
}
