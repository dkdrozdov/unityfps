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
    [SerializeField] ToolUser toolUser;

    void Start()
    {
        InitializeUIManager();
        GetPlayerComponents();

        InitializeHealth();
        InitializeDamageable();
        InitializeEnergy();
        InitializePlayerMove();
        InitializeToolUser();
    }
    void GetPlayerComponents()
    {
        health = player.GetComponent<Health>();
        damageable = player.GetComponent<Destructible>();
        energy = player.GetComponent<Energy>();
        playerMove = player.GetComponent<PlayerMove>();
    }
    void InitializeToolUser()
    {
        if (IsOwner)
        {
            uiManager.SetToolUser(toolUser);
        }
    }
    void InitializePlayerMove()
    {
        playerMove.SetEnergy(energy);
        playerMove.SetController(characterController);
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
        damageable.SetHealthComponent(health);
    }
    void InitializeEnergy()
    {
        if (IsServer)
        {
            energy.SetMaxValue(maxEnergy);
            energy.SetValue(maxEnergy);
        }
        if (IsOwner)
        {
            uiManager.GetEnergyBar().SetStat(energy);
        }
        energy.SetPlayerMove(playerMove);
    }
    void InitializeHealth()
    {
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