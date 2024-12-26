using System;
using System.IO;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public static string SaveFileName = "SaveFile.json";

    public static void SaveData()
    {
        string path = Path.Combine(Application.persistentDataPath, SaveFileName);

        PlayerData data = new();

        File.WriteAllText(path, JsonUtility.ToJson(data));
    }

    public static void LoadData()
    {
        string path = Path.Combine(Application.persistentDataPath, SaveFileName);

        PlayerData data = new();

        if (File.Exists(path))
        {
            data = JsonUtility.FromJson<PlayerData>(File.ReadAllText(path));
        }
    }
}

[Serializable]
public class PlayerData 
{

}