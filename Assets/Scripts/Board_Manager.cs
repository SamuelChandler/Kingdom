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

    [Serializable]
    public class Board
    {
        public int columns;
        public int rows;

        public GameObject[] floorTiles;

        private List<Vector3> gridPositions = new List<Vector3>();

        public Board(int col,int row)
        {
            columns = col; rows = row;

            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    gridPositions.Add(new Vector3(i, j, 0f));
                }
            }
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
