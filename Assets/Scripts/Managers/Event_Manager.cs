using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Event_Manager : MonoBehaviour
{
    public delegate void Refresh();

    public delegate void OnAllyDeath(BaseUnit h);


    public static event Refresh OnRefresh;

    public static event OnAllyDeath onAllyDeath;

    public static Event_Manager instance;

    private void Awake()
    {
        instance = this;

        OnRefresh += BaseRefresh;
        onAllyDeath += BaseOnAllyDeath;
    }

    public void BaseRefresh(){
        Debug.Log("RefreshEvent");
    }

    public void BaseOnAllyDeath(BaseUnit h){
        Debug.Log("Ally Death Event");
    }

    public void refresh()
    {
        OnRefresh();
    }

    public void AllyDeath(BaseUnit h){
        onAllyDeath(h);
    }
}
