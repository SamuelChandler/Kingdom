using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class NPC : MonoBehaviour, IInteractable
{

    [SerializeField] private SpriteRenderer _interactSprite;

    private Transform _playerTransform;

    [SerializeField] private float INTERACT_DISTANCE = 5f;

    [SerializeField] private int _despawnEvent;

    private void Start()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        
        
    }

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

        if(!IsWithinInteractDistance()){
            StopInteracting();
        }

        if (_interactSprite.gameObject.activeSelf && !IsWithinInteractDistance())
        {
            //turn off sprite
            _interactSprite.gameObject.SetActive(false);

        }
        else if (!_interactSprite.gameObject.activeSelf && IsWithinInteractDistance())
        {
            //turn on sprite
            _interactSprite.gameObject.SetActive(true);
        }

        //check if despawn event completed and despawn if it is
        if(DataPersistanceManager.instance.CheckEvent(_despawnEvent)){
            Destroy(gameObject);
        }
    }

    public abstract void Interact();

    public abstract void StopInteracting();

    public abstract void ResolveChoice(bool c);

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
