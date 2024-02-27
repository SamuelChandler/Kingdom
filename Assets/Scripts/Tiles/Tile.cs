using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tile : MonoBehaviour
{
    
    [SerializeField] protected SpriteRenderer _renderer;
    [SerializeField] protected GameObject _highlight;

    public virtual void Init(int x,int y)
    {
        _highlight.SetActive(false);
    }

    private void OnMouseEnter()
    {
        _highlight.SetActive(true);
    }

    private void OnMouseExit()
    { 
        _highlight.SetActive(false); 
    }

}
