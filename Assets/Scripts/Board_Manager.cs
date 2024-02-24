using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Board_Manager : MonoBehaviour
{

    //serialized fields
    [Serializable]
    public class Count 
    {
        public int minimum;
        public int maximum;

        public Count (int min, int max)
        {
            minimum = min;
            maximum = max;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
