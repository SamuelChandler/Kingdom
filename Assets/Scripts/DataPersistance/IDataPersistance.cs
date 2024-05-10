using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataPersistance 
{
    void LoadData(PlayerData playerData);

    void SaveData(ref PlayerData playerData);

}

   
