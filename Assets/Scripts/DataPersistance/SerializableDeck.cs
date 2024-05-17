using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializableDeck : Deck, ISerializationCallbackReceiver
{

    [SerializeField] string s_name;

    public SerializableDeck(string n) : base(n)
    {
    }

    public void OnAfterDeserialize()
    {
        s_name = this.name;
        
    }

    public void OnBeforeSerialize()
    {
        throw new System.NotImplementedException();
    }
}
