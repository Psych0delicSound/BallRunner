using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveRecord
{
    string savePath = Application.persistentDataPath + "/SaveData.dat";


    int bestScore;

    public int Comparing(int score)
    {
        LoadGame();
        if (score > bestScore)
        {
            bestScore = score;
        }
        SaveGame();
        return bestScore;
    }
    void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(savePath);
        SaveData data = new SaveData();
        data.savedBestScore = bestScore;

        bf.Serialize(file, data);
        file.Close();
    }
    void LoadGame()
    {
        if (File.Exists(savePath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(savePath, FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();
            bestScore = data.savedBestScore;
        }
    }

    public void IsNull(String _text, String _string)
    {
        if (_string != null)
        {
            _text = _string;
        }
        else
        {
            _text = "";
        }
    }
}

[Serializable]
class SaveData
{
    public int savedBestScore;
}
