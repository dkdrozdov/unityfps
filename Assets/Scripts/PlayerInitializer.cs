using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
public class PlayerInitializer : NetworkBehaviour
{
    [SerializeField] float maxHP = 100f;
    [SerializeField] float maxEnergy = 100f;
    [SerializeField] CharacterController characterController;
    [SerializeField] Transform groundCheck;
    [SerializeField] PlayerMove playerMove;
    [SerializeField] GameObject player;
    [SerializeField] GameObject uiPrefab;
    [SerializeField] IDamageable damageable;
    [SerializeField] Health health;
    [SerializeField] Energy energy;
    [SerializeField] UIManager uiManager;

    void Start()
    {
        InitializeUIManager();
        InitializeHealth();
        InitializeDamageable();
        // InitializeEnergy();
    }
    void InitializePlayerMove()
    {
        playerMove = player.AddComponent<PlayerMove>();
        playerMove.SetEnergy(energy);
    }
    void InitializeUIManager()
    {
        if (IsOwner)
        {
            uiManager = Instantiate(uiPrefab, Vector3.zero, Quaternion.identity).GetComponent<UIManager>();
        }
    }
    void InitializeDamageable()
    {
        damageable = player.GetComponent<Destructible>();
        damageable.SetHealthComponent(health);
    }
    void InitializeEnergy()
    {
        energy = player.AddComponent<Energy>();
        energy.SetMaxValue(maxEnergy);
        energy.SetValue(maxEnergy);
        uiManager.GetEnergyBar().SetStat(energy);
        InitializePlayerMove();
        energy.SetPlayerMove(playerMove);
    }
    void InitializeHealth()
    {
        health = player.GetComponent<Health>();
        if (IsServer)
        {
            health.SetMaxValue(maxHP);
            health.SetValue(maxHP);
        }
        if (IsOwner)
        {
            uiManager.GetHealthBar().SetStat(health);
        }
    }
    public UIManager GetUI()
    {
        return uiManager;
    }
    public PlayerMove GetPlayerMove()
    {
        return playerMove;
    }
    public CharacterController GetCharacterController()
    {
        return characterController;
    }
    public Transform GetGroundCheck()
    {
        return groundCheck;
    }
}