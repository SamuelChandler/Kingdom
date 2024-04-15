using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_Manager : MonoBehaviour
{
    public delegate void Refresh();
    public static event Refresh OnRefresh;

    public static Event_Manager instance;

    private void Awake()
    {
        instance = this;
    }

    public void refresh()
    {
        OnRefresh();
    }
}
