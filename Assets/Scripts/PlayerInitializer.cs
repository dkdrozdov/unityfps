using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
public class PlayerInitializer : NetworkBehaviour
{
    public CharacterController characterController;
    public Transform groundCheck;
    public PlayerMove playerMove;
    public GameObject player;
    public GameObject uiPrefab;
    UIManager uiManager;

    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        playerMove = player.AddComponent<PlayerMove>();
        if (IsOwner)
        {
            uiManager = Instantiate(uiPrefab, Vector3.zero, Quaternion.identity).GetComponent<UIManager>();
            playerMove.SetEnergy(player.AddComponent<PlayerEnergy>());
        }
        else
        {
            playerMove.SetEnergy(player.AddComponent<Energy>());
        }
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
