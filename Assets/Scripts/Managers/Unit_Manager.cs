using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Manager : MonoBehaviour
{
    public static Unit_Manager instance;

    private void Awake()
    {
        instance = this;
    }


}
