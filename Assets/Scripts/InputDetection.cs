using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputDetection : MonoBehaviour
{

    [SerializeField] BattlePauseMenu _pauseMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Escape)){
            if (!_pauseMenu.isPaused)
            {
                _pauseMenu.Show();
            }
            else if (_pauseMenu.isPaused)
            {
                _pauseMenu.Hide();
            }
        }
    }
}
