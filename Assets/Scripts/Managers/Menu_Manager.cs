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

    [SerializeField] private GameObject _displayCardObject, _tileInfo, _tileUnit, _messanger, _unitSelect,_winScreen,_lossScreen;


    [SerializeField] private HeroCardFrame _displayCard;

    [SerializeField] private Button _endTurn;

    [SerializeField] InspirationBar _iBar;

    [SerializeField] float _displayTime;
    [SerializeField] float _fadeOutTime;




    private void Awake()
    {
        instance = this;

        //clear unessisary windows 
        _displayCardObject.SetActive(false);
        _tileInfo.SetActive(false);
        _messanger.SetActive(false);
        _winScreen.SetActive(false);
        _lossScreen.SetActive(false);

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

    //displays tile info on screen when called. null clears window
    public void showTileInfo(Tile tile)
    {
        //toggle off
        if (tile == null)
        {
            _tileInfo.SetActive(false);
            _tileUnit.SetActive(false);
            return;
        }

        //display name of tile if selected
        _tileInfo.GetComponentInChildren<TextMeshProUGUI>().text = tile.tileName;
        _tileInfo.SetActive(true);


        // display unit on tile if there is one 
        if (tile.OccupiedUnit)
        {
            _tileUnit.GetComponentInChildren<TextMeshProUGUI>().text = tile.OccupiedUnit.unit.name + "\n" + tile.OccupiedUnit.currentHealth + "/" + tile.OccupiedUnit.unit.health;
            _tileUnit.SetActive(true);
        }

        if (tile.OccupiedStructure){
            _tileUnit.GetComponentInChildren<TextMeshProUGUI>().text = tile.OccupiedStructure._structure.name + "\n" + tile.OccupiedStructure.currentHealth 
                                                                        + "/" + tile.OccupiedStructure._structure.health;
            _tileUnit.SetActive(true);
        }
    }

    //displays the hero information when called. null clears window 
    public void showUnit(BaseHero hero)
    {
        //toggle off 
        if (hero == null)
        {
            _displayCardObject.SetActive(false);
            return;
        }

        _displayCard.setCard(hero);

        //display selected hero
        _displayCardObject.SetActive(true);
    }

    public void showUnit(ScriptableUnit unit)
    {
        //toggle off 
        if (unit == null)
        {
            _displayCardObject.SetActive(false);
            return;
        }

        //build text based on hero
        _displayCard.setCard(unit);

        _displayCardObject.SetActive(true);
    }


    public void showWinScreen(){
        if(_winScreen != null){
            _winScreen.SetActive(true);
        }
    }

    public void showLossScreen(){
        if(_lossScreen != null){
            _lossScreen.SetActive(true);
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
}