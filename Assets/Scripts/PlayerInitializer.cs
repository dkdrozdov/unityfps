using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
public class PlayerInitializer : NetworkBehaviour
{
    [SerializeField] float maxHP = 100f;
    public CharacterController characterController;
    public Transform groundCheck;
    public PlayerMove playerMove;
    public GameObject player;
    public GameObject uiPrefab;
    public IDamageable damageable;
    public Health health;
    UIManager uiManager;

    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        // playerMove = player.AddComponent<PlayerMove>();
        if (IsOwner)
        {
            InitializeUIManager();
            // playerMove.SetEnergy(player.AddComponent<PlayerEnergy>());
            InitializeHealth();
            InitializeDamageable();
        }
        else
        {
            playerMove.SetEnergy(player.AddComponent<Energy>());
            damageable.SetHealthComponent(player.AddComponent<Health>());
        }
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
    void InitializeHealth()
    {
        health = player.AddComponent<Health>();
        health.SetMaxValue(maxHP);
        health.SetValue(maxHP);
        uiManager.GetHealthBar().SetStat(health);
    }
    // Update is called once per frame
    void Update()
    {

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
