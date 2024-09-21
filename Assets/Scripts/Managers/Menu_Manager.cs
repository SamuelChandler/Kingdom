/*
 * Used to manage the UI elements in the game scene 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Menu_Manager : MonoBehaviour
{
    public static Menu_Manager instance;

    [SerializeField] private GameObject _displayCardObject, _tileInfo, _GameGoal, _messanger;

    [SerializeField] private TextMeshProUGUI _tileName, _tileDesc;

    [SerializeField] private WinScreen _winScreen;
    [SerializeField] private LossScreen _lossScreen;
    [SerializeField] GameObject _battleTutorial;

    [SerializeField] private UnitSelector[] unitSelectors;

    [SerializeField] private HeroCardFrame _displayCard;

    [SerializeField] private Button _endTurn;

    [SerializeField] InspirationBar _iBar;

    [SerializeField] float _displayTime;
    [SerializeField] float _fadeOutTime;

    

    public UnitSelector CurrentSelectedSelector;

    private void Awake()
    {
        instance = this;

        //clear unessisary windows 
        _displayCardObject.SetActive(false);
        _tileInfo.SetActive(false);
        _GameGoal.SetActive(false);
        _messanger.SetActive(false);
        _battleTutorial.SetActive(false);
        SetUnitSelectorsToEmpty();

    }

    public void setBattleTutorial(bool b){
        _battleTutorial.SetActive(b);
    }

    public void UpdateIBar(int i, int j, int k)
    {
        //set and update in insiration bar on awake
        _iBar.SetInsperation(i, j, k);
        _iBar.UpdateDisplay();
    }

    //sets the banner ot the bottom of the screen to warn the player or give info
    public void SetMessenger(string message)
    {
        //if there is no message remove the message box 
        if (message == null || message.Length == 0)
        {
            _messanger.SetActive(false);
            return;
        }

        _messanger.GetComponentInChildren<TextMeshProUGUI>().text = message;
        _messanger.SetActive(true);

        StartCoroutine(DisplayBeforeFadeOut(_messanger));
    }

    public void showGameGoal(string s){
        if(s == null){
            _GameGoal.SetActive(false);
        }

        _GameGoal.GetComponentInChildren<TextMeshProUGUI>().text = s;
        _GameGoal.SetActive(true);

        StartCoroutine(DisplayBeforeFadeOut(_GameGoal));
    }

    //displays tile info on screen when called. null clears window
    public void showTileInfo(Tile tile)
    {
        //toggle off
        if (tile == null)
        {
            _tileInfo.SetActive(false);
            
            return;
        }

        //display name of tile if selected
        _tileName.text = tile.tileName;
        _tileDesc.text = tile.tileDesc;
        _tileInfo.SetActive(true);
    }

    //displays the hero information when called. null clears window 
    public void showUnit(BoardObject obj)
    {
        //toggle off 
        if (obj == null)
        {
            _displayCardObject.SetActive(false);
            return;
        }

        _displayCard.setCard(obj);
        _displayCardObject.SetActive(true);
    }

    public void showCard(Card c){
        if(c == null){
            _displayCardObject.SetActive(false);
            return;
        }

        //build text based on hero
        _displayCard.setCard(c);
        _displayCardObject.SetActive(true);
    }


    public void showWinScreen(Card c){
        if(_winScreen != null){
            _winScreen.ShowWinScreen(c);
        }else{
            Debug.Log("Win Screen is null");
        }
    }

    public void showLossScreen(){
        if(_lossScreen != null){
            _lossScreen.ShowLossScreen();
        }else{
            Debug.Log("Loss Screen is null");
        }
        
    }

    IEnumerator DisplayBeforeFadeOut(GameObject o){
        float timeElapsed = 0f;

        while(timeElapsed < _displayTime){
            //do nothing 
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        StartCoroutine(FadeOut(o));

    }

    IEnumerator FadeOut(GameObject o){
        float timeElapsed = 0f;
        Color c = o.GetComponent<Image>().color;
        Color textc = o.GetComponentInChildren<TextMeshProUGUI>().faceColor;
        //fade out 
        while(timeElapsed < _fadeOutTime){
            
            c =  new Color(c.r,c.g,c.b,1 - timeElapsed/_fadeOutTime);
            textc = new Color(textc.r,textc.g,textc.b,1 - timeElapsed/_fadeOutTime);

            o.GetComponentInChildren<TextMeshProUGUI>().faceColor = textc;
            o.GetComponent<Image>().color = c;

             timeElapsed += Time.deltaTime;
            yield return null;
        }

        o.SetActive(false);
        o.GetComponentInChildren<TextMeshProUGUI>().faceColor = new Color(textc.r,textc.g,textc.b,255f);
        o.GetComponent<Image>().color = new Color(c.r,c.g,c.b,255f);
    }

    public void SetUnitSelectorsToEmpty(){
        foreach(UnitSelector s in unitSelectors){
            s.ClearCard();
        }
    }

    //sets the first selector it finds to a unit, used for drawing 
    public void SetSelectorToCard(Card c){
        foreach(UnitSelector s in unitSelectors){
            if(s.held_card == null){
                s.SetCard(c);
                return;
            }
        }

        Debug.Log("No space in hand");
    }

}