using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class Door : MonoBehaviour, IInteractable
{

    [SerializeField] private GameObject DoorIndicator;

    private Transform _playerTransform;

    [SerializeField] private float INTERACT_DISTANCE = 5f;

    [SerializeField] private Door nextDoor;

    [SerializeField] private Vector3 _teleportOffset;

    [SerializeField] private GameObject _myCamera;

    public void Interact()
    {
        Debug.Log(name + " has been interacted with");

        _myCamera.SetActive(false);

        nextDoor.TeleportPlayerToMe();

    }

    public void TeleportPlayerToMe(){
        
        Vector3 dest = transform.position + _teleportOffset;

        _myCamera.SetActive(true);

        Player.instance.TeleportPlayer(dest);
    }

    // Start is called before the first frame update
    void Start()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        DoorIndicator.SetActive(false);
    }

    // Update is called once per frame
    protected void Update()
    {
        //check it the user wants to interact.
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            //interact method 
            if (IsWithinInteractDistance())
            {
                Interact();
            }
        }

        if (!IsWithinInteractDistance())
        {
            //normal material
            DoorIndicator.SetActive(false);; 

        }
        else if (IsWithinInteractDistance())
        {
            //swap to selected Material
            DoorIndicator.SetActive(true);; 
        }
    }

    private bool IsWithinInteractDistance()
    {
        if (Vector2.Distance(_playerTransform.position, transform.position) < INTERACT_DISTANCE)
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }
}
