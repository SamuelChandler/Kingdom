using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InspirationBar : MonoBehaviour
{

    [SerializeField] public GameObject[] ticks;

    int CurrentInspiration;
    int MaxTurnInspiration;
    int MaxInspiration;

    [SerializeField] Color Ready;
    [SerializeField] Color Used;
    [SerializeField] Color NotUsable;


    public void UpdateDisplay()
    {
        for (int i = 0; i < ticks.Length; i++)
        {
            if (ticks[i] != null)
            {
                if (i < MaxTurnInspiration)
                {
                    ticks[i].GetComponent<Image>().color = Used;
                    if (i < CurrentInspiration) { ticks[i].GetComponent<Image>().color = Ready; }
                }
                else if (i < MaxInspiration)     { ticks[i].GetComponent<Image>().color = NotUsable; }
            }
        }
    }

    public void SetInsperation(int i, int j,int k)
    {
        Debug.Log("Changing inspiration");
        CurrentInspiration = i;
        MaxTurnInspiration = j;
        MaxInspiration = k;
    }


    public void SetMaxInspiration(int i)
    {
        MaxInspiration = i;
    }

    public void SetMaxTurnInsperation(int i)
    {

        //reduce to max inspiration
        if (MaxInspiration < i) { i = MaxInspiration; }

        MaxTurnInspiration = i;
    }

    public void SetCurrentInspiration(int i)
    {
        if (MaxTurnInspiration < i) { i = MaxTurnInspiration; }

        CurrentInspiration = i;
    }



}
