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

    [SerializeField] private GameObject _SelectedHeroObject, _tileInfo, _tileUnit, _messanger, _unitSelect;

    [SerializeField] InspirationBar _iBar;

    //there are 4 unit select buttons for now



    private void Awake()
    {
        instance = this;

        //clear unessisary windows 
        _SelectedHeroObject.SetActive(false);
        _tileInfo.SetActive(false);
        //_unitSelect.SetActive(false);
        _messanger.SetActive(false);

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
    public void showSelectedHero(BaseHero hero)
    {
        //toggle off 
        if (hero == null)
        {
            _SelectedHeroObject.SetActive(false);
            return;
        }

        //build text based on hero
        string displayedText = hero.unit.name;
        displayedText += "\nCost: " + hero.unit.inspirationCost.ToString();
        displayedText += "\nHealth: " + hero.currentHealth.ToString() + "/" + hero.unit.health.ToString();
        displayedText += "\nSpeed: " + hero.unit.speed.ToString() + "   Attack: " + hero.unit.attack.ToString();

        //display selected hero
        _SelectedHeroObject.GetComponentInChildren<TextMeshProUGUI>().text = displayedText;
        _SelectedHeroObject.SetActive(true);
    }

    public void showSelectedHero(ScriptableUnit unit)
    {
        //toggle off 
        if (unit == null)
        {
            _SelectedHeroObject.SetActive(false);
            return;
        }

        //build text based on hero
        string displayedText = unit.name;
        displayedText += "\nCost: " + unit.inspirationCost.ToString();
        displayedText += "\nHealth: "+ unit.health.ToString();
        displayedText += "\nSpeed: " + unit.speed.ToString() + "   Attack: " + unit.attack.ToString();

        //display selected hero
        _SelectedHeroObject.GetComponentInChildren<TextMeshProUGUI>().text = displayedText;
        _SelectedHeroObject.SetActive(true);
    }

}