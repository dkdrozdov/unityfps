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
        if (IsOwner)
        {
            InitializeUIManager();
            InitializeHealth();
            InitializeDamageable();
            InitializeEnergy();
        }
        else
        {
            playerMove.SetEnergy(player.AddComponent<Energy>());
            damageable.SetHealthComponent(player.AddComponent<Health>());
        }
    }
    void InitializePlayerMove()
    {
        playerMove = player.AddComponent<PlayerMove>();
        playerMove.SetEnergy(energy);
    }
    void InitializeUIManager()
    {
        uiManager = Instantiate(uiPrefab, Vector3.zero, Quaternion.identity).GetComponent<UIManager>();
    }
    void InitializeDamageable()
    {
        damageable = player.AddComponent<Destructible>();
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
        health = player.AddComponent<Health>();
        health.SetMaxValue(maxHP);
        health.SetValue(maxHP);
        uiManager.GetHealthBar().SetStat(health);
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