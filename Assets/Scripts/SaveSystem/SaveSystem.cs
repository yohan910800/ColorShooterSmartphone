using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem{


    public static void SaveStats(Stats stats)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/playerSavedata.test";
        //string path2=Application.dataPath+"/savedata/playerSavedata.test"; //== This path is for testing. We should set a new path for an actual game
        FileStream stream = new FileStream(path,FileMode.Create);
        formatter.Serialize(stream, stats);
        stream.Close();
    }
    public static Stats LoadStats()
    {
        string path = Application.persistentDataPath + "/playerSavedata.test";
        //string path2=Application.dataPath+"/savedata/playerSavedata.test";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

          Stats data= formatter.Deserialize(stream) as Stats;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }

     public static void SaveAutoSaveData(AutoSaveData data ){
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/playerSavedata.test";
        //string path2=Application.dataPath+"/savedata/autoSaveData.test"; //== This path is for testing. We should set a new path for an actual game
        FileStream stream = new FileStream(path,FileMode.Create);
        formatter.Serialize(stream, data);
        stream.Close();
    }

     public static AutoSaveData LoadAutoSaveData(){
        string path = Application.persistentDataPath + "/playerSavedata.test";
        //string path2=Application.dataPath+"/savedata/autoSaveData.test";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

          AutoSaveData data= formatter.Deserialize(stream) as AutoSaveData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null; 
        }
    }

    public static void Savehubsavedata(HubSaveData data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/playerSavedata.test";
        //string path2 = Application.dataPath + "/savedata/HubData.test"; //== This path is for testing. We should set a new path for an actual game
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static HubSaveData Loadhubsavedata()
    {
        string path = Application.persistentDataPath + "/playerSavedata.test";
        //string path2 = Application.dataPath + "/savedata/HubData.test";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            HubSaveData data = formatter.Deserialize(stream) as HubSaveData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }

    public static void SaveInventory(Inventory data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/playerSavedata.test";
        //string path2 = Application.dataPath + "/savedata/InventoryData.test"; //== This path is for testing. We should set a new path for an actual game
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static Inventory LoadInventory()
    {
        string path = Application.persistentDataPath + "/playerSavedata.test";
        //string path2 = Application.dataPath + "/savedata/InventoryData.test";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            Inventory data = formatter.Deserialize(stream) as Inventory;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }

}
