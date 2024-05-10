using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using System.IO;

public class DataPersistanceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;

    private PlayerData playerData;

    private List<IDataPersistance> dataPersistanceObjects;
    private FileDataHandler dataHandler; 


    public static DataPersistanceManager instance {get; private set; }

    private void Awake(){

        if(instance != null){
            Debug.Log("Too Many Data Managers");
        }

        instance = this;
    }

    public void NewGame(string playerName){
        this.playerData = new PlayerData(playerName);
    }

    public void LoadGame(){
        //TD Load any Saved Data from a file from the data handler
        this.playerData = dataHandler.Load();

        //if there is no data then start a new game
        if(this.playerData == null){
            Debug.Log("No Data Found");
            NewGame("NewPlayer");
        }

        foreach(IDataPersistance dpObject in dataPersistanceObjects){
            dpObject.LoadData(playerData);
        }

    }

    public void SaveGame(){
        //TD pass data to other scripts so they can update it
        foreach(IDataPersistance dpObject in dataPersistanceObjects){
            dpObject.SaveData(ref playerData);
        }

        //TD save that data to a file using the data handler
        dataHandler.Save(playerData);
    }

    private void Start(){
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.dataPersistanceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }

    private List<IDataPersistance> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistance> dataPersistanceObjects = FindObjectsOfType<MonoBehaviour>()
            .OfType<IDataPersistance>();

        return new List<IDataPersistance>(dataPersistanceObjects);
    }

    private void OnApplicationQuit(){
        SaveGame();
    }
}
