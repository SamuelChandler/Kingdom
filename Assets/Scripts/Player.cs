using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, IDataPersistance
{

    public string PlayerName;

    public float moveSpeed = 5f;

    public Rigidbody2D rb;

    private Vector2 movement;

    [SerializeField]
    public PauseMenu _pauseMenu;

    public PlayerData data;

    public static Player instance;

    public Deck SelectedDeck;

    [SerializeField]
    private Animator animator;

    void Awake(){
        instance = this;
        animator = GetComponent<Animator>();

        
    }

    public void SetSelectedDeck(Deck d){
        SelectedDeck = d;
        data.SelectedDeck = d.name;
        _pauseMenu.ShowDecks(data.SelectedDeck);
    }

    public void SetDirection(Vector2 movmentVector){

        float x = Math.Abs(movmentVector.normalized.x);
        float y = Math.Abs(movmentVector.normalized.y);

        if(y ==0 && x ==0){
            animator.SetInteger("GoState",0);
            return;
        }

        if(x >= y){
            if(movmentVector.x > 0){
                animator.SetInteger("GoState",3);
                return;
            }else{
                animator.SetInteger("GoState",4);
                return;
            }
        }else{
            if(movmentVector.y > 0){
                animator.SetInteger("GoState",2);
                return;
            }else{
                animator.SetInteger("GoState",1);
                return;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {

        movement = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));

        SetDirection(movement);

        if(Keyboard.current.escapeKey.wasPressedThisFrame){
            _pauseMenu.PauseGame();
        }

    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + (movement.normalized * moveSpeed * Time.fixedDeltaTime));
    }

    public void LoadData(PlayerData playerData)
    {
        this.data._decks = playerData._decks;
        this.data._deckContents = playerData._deckContents;
        this.data._cardInventory = playerData._cardInventory;
        this.data.PlayerName = playerData.PlayerName;
        this.data.SelectedDeck = playerData.SelectedDeck;
        gameObject.transform.position = playerData.MapLocation;

        SetSelectedDeck(playerData.GetDeck(data.SelectedDeck));
        PlayerName = playerData.PlayerName;

        if(PlayerPrefs.GetString("FromDeckbuiler") == "Yes"){
            _pauseMenu.PauseGame();
            _pauseMenu.ShowDecks(data.SelectedDeck);
            PlayerPrefs.SetString("FromDeckbuiler","No");
        }

    }

    public void SaveData(ref PlayerData playerData)
    {
        playerData._decks = this.data._decks;
        playerData._deckContents = this.data._deckContents;
        playerData._cardInventory = this.data._cardInventory;
        playerData.PlayerName = this.data.PlayerName;
        playerData.SelectedDeck = data.SelectedDeck;
        playerData.CombatMap = data.CombatMap;
        playerData.MapLocation = (Vector2)gameObject.transform.position;
    }

    
}


