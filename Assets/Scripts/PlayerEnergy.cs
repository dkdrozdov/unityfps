using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.NetworkVariable;

public class PlayerEnergy : Energy
{
    public StatBar energyBar;
    // protected override void Awake()
    // {
    //     PlayerInitializer playerInitializer = gameObject.GetComponent<PlayerInitializer>();
    //     playerMove = playerInitializer.GetPlayerMove();
    //     energyBar = playerInitializer.GetUI().GetEnergyBar();
    // }
    void Start()
    {
        if (IsOwner)
        {
            // energyBar.SetMaxValue(maxValue.Value);
        }
    }
    protected override void ModifyValue(float value)
    {
        if (IsOwner)
        {
            base.ModifyValue(value);
            // energyBar.SetValue(currentValue.Value);
        }
    }
}
