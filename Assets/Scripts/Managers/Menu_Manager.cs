using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Menu_Manager : MonoBehaviour
{
    public static Menu_Manager instance;

    [SerializeField] private GameObject _SelectedHeroObject,_tileInfo,_tileUnit;

    
    private void Awake()
    {
        instance = this;
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
            _tileUnit.GetComponentInChildren<TextMeshProUGUI>().text = tile.OccupiedUnit.UnitName;
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
        string displayedText = hero.UnitName;
        displayedText += "\nHealth: " + hero.Health.ToString() + "/" + hero.MaxHealth.ToString();
        displayedText += "\nSpeed: " + hero.Speed.ToString() + "   Attack: " + hero.attack.ToString();

        //display selected hero
        _SelectedHeroObject.GetComponentInChildren<TextMeshProUGUI>().text = displayedText;
        _SelectedHeroObject.SetActive(true);
    }
}
