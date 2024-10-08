using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Enumeration;
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

    [SerializeField]
    private GameObject _tutorial;

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
        _pauseMenu.ShowDecks(d.name);
    }

    // Update is called once per frame
    void Update()
    {
        
        movement = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));

        //setting animator variables
        animator.SetFloat("xMovement",movement.normalized.x);
        animator.SetFloat("yMovement", movement.y);
        animator.SetFloat("Speed",movement.sqrMagnitude);


        if(Keyboard.current.escapeKey.wasPressedThisFrame){
            if (!_pauseMenu.isPaused)
            {
                _pauseMenu.PauseGame();
            }
            else if (_pauseMenu.isPaused)
            {
                _pauseMenu.ContinueGame();
            }
        }

    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + (movement.normalized * moveSpeed * Time.fixedDeltaTime));
    }

    public void TeleportPlayer(Vector3 newPosition){
        transform.position = newPosition;
    }
    
    public void LoadData(PlayerData playerData)
    {
        data = playerData;
        gameObject.transform.position = playerData.MapLocation;

        SetSelectedDeck(playerData.GetDeck(data.SelectedDeck));
        PlayerName = playerData.PlayerName;

        if(PlayerPrefs.GetString("FromDeckbuiler") == "Yes"){
            _pauseMenu.PauseGame();
            _pauseMenu.ShowDecks(data.SelectedDeck);
            PlayerPrefs.SetString("FromDeckbuiler","No");
        }

        TutorialCheck();

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
        playerData.seenOverWorldTutorial = data.seenOverWorldTutorial;
    }

    //determines if the tutorial is needed and displays it if necessary
    public void TutorialCheck(){
        if(!data.seenOverWorldTutorial){
            Debug.Log("Displaying overworld tutorial" + !data.seenOverWorldTutorial );
            _tutorial.SetActive(true);
            data.seenOverWorldTutorial =true;
            return;
        }
        _tutorial.SetActive(false);
    }

    
}


