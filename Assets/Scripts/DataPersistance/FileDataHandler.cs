using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileDataHandler 
{
    private string dataDirPath = "";

    private string dataFileName = "";


    public FileDataHandler(string dataDirPath, string dataFileName){
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;

    }

    public PlayerData Load(){
        //combine the directory path and file name for the full path of the saved data 
        string fullPath = Path.Combine(dataDirPath,dataFileName);

        PlayerData loadedData = null; 

        if(File.Exists(fullPath)){
            try {
                //load file data to string 
                string dataToLoad = "";

                using(FileStream s = new FileStream(fullPath,FileMode.Open)){
                    using (StreamReader reader = new StreamReader(s)){
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                //deserialize json to data
                Debug.Log(dataToLoad);
                loadedData = JsonUtility.FromJson<PlayerData>(dataToLoad);

            }catch(Exception e){
                Debug.LogError("Error occured when trying to load data from file: "+fullPath+"\n"+e);
            }
        }
        Debug.Log(fullPath);

        return loadedData;
    }

    public void Save(PlayerData playerData){

        //combine the directory path and file name for the full path of the saved data 
        string fullPath = Path.Combine(dataDirPath,dataFileName);

        try{
            //create the directory the file will be stored in if it does not exist 
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            //Serialize C# game data object into Json 
            string dataToStore = JsonUtility.ToJson(playerData,true);
            Debug.Log(dataToStore);

            //write the serializable data to the file
            using (FileStream stream = new FileStream(fullPath,FileMode.Create)){
                using (StreamWriter writer = new StreamWriter(stream)){
                    writer.Write(dataToStore);
                }
            }
        }
        catch(Exception e){
            Debug.LogError("Error occured when trying to write to file: " + fullPath + "\n" + e);
        }


    }
}
