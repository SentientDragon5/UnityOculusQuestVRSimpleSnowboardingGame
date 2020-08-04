/* This Script is meant to save data (floats, ints, strings, bools) to a binary file that I read and write in this file.
 * I also can read and Save from other scripts.
 * 
 * I used content and code from this video - some of this code is Colanderp's 
 * https://www.youtube.com/watch?v=BPu3oXma97Y - Create a Save and Load System in Unity! - By Colinderp 
 * 
 * When understanding this I found this Brackeys tutorial helpful in concept, though the code did not work in my instance.
 * They both use Binary formaters to save and load Floats, Ints, Strings, and Bools. Other data types (like Vector3) can be stored as arrays of floats.
 * https://www.youtube.com/watch?v=XOjd_qU2Ido - SAVE & LOAD SYSTEM in Unity - By Brackeys
 * 
 */
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class LoadManager : MonoBehaviour
{
    public static LoadManager instance; // instance is used to ensure there is only ONE instance of this LoadManager, in the case of two scenes being loaded at the same time and there being an overlap with 2 LoadManagers.
    public SaveData data; // This is the actual data being stored the class, SaveData, is defined below.
    string dataFile = "SaveData.dat"; // what the file will be called whether being created or appended.
    void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);//Don't Destroy is a separate scene that opens on load, this line moves ONE instance of LoadManager into Don't Destory
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Load();
    }

    // Update is called once per frame
    void Update()
    {
        
        Save();
    }
    public void Save()
    {
        string filepath = Application.persistentDataPath + "/" + dataFile;//We use persistantDataPath because it always works, regardless of whether Windows, or Android, ect.
        BinaryFormatter Bf = new BinaryFormatter();
        FileStream file = File.Create(filepath);
        Bf.Serialize(file, data);
        file.Close();// ALWAYS close the file
    }
    public void Load()
    {
        string filepath = Application.persistentDataPath + "/" + dataFile;
        BinaryFormatter Bf = new BinaryFormatter();
        if (File.Exists(filepath))
        {
            FileStream file = File.Open(filepath,FileMode.Open);
            SaveData loaded = (SaveData)Bf.Deserialize(file);
            data = loaded;
            file.Close();
        }
        
    }
    public void Reset()//This was my attempt at a Reset function. It does work, but (at least on the Oculus Quest) it crashes the app
    {
        data.Coins = 0;
        Save();
    }
}

[System.Serializable]
public class SaveData // this class is of each data that I needed
{
    public int Coins = 0;
    [Range(0, 1f)] public float volumeMusic = 0.5f;
    [Range(0, 1f)] public float volumeSFX = 0.5f;

    public SaveData()
    {
        Coins = 0;
        volumeMusic = 0.5f;
        volumeSFX = 0.5f;
    }
}
