using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, IDataPersistance
{

    public string PlayerName;

    public float moveSpeed = 5f;

    public Rigidbody2D rb;

    private Vector2 movement;

    [SerializeField]
    private PauseMenu _pauseMenu;

    public PlayerData data;


    // Update is called once per frame
    void Update()
    {

        //get user movment input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if(Keyboard.current.escapeKey.wasPressedThisFrame){
            _pauseMenu.PauseGame();
        }

    }



    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + (movement * moveSpeed * Time.fixedDeltaTime));
    }

    public void LoadData(PlayerData playerData)
    {
        this.data._decks = playerData._decks;
        this.data._cardInventory = playerData._cardInventory;
        this.data.PlayerName = playerData.PlayerName;
        PlayerName = playerData.PlayerName;

    }

    public void SaveData(ref PlayerData playerData)
    {
        playerData._decks = this.data._decks;
        playerData._cardInventory = this.data._cardInventory;
        playerData.PlayerName = this.data.PlayerName;
    }
}
