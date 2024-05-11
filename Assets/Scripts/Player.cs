using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, IDataPersistance
{
    public float moveSpeed = 5f;

    public Rigidbody2D rb;

    private Vector2 movement;

    [SerializeField]
    private PauseMenu _pauseMenu;

    public PlayerData data;

    public Card cardToSave;

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
        data._cardInventory.Add(cardToSave);
        data._cardInventory.Add(cardToSave);
    }

    public void SaveData(ref PlayerData playerData)
    {
        playerData._decks = this.data._decks;
        playerData._cardInventory = this.data._cardInventory;
    }
}
