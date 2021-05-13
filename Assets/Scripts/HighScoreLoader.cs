using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public class HighScore
{
    public int highScore;
}

public class HighScoreLoader : MonoBehaviour
{
    private int highScore;

    private void Awake()
    {
        Input.backButtonLeavesApp = true;
        LoadScore();
    }

    public void SaveScore()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/HighScore.sav");
        HighScore hs = new HighScore { highScore = highScore };

        bf.Serialize(file, hs); 
        file.Close();
    }

    public void LoadScore()
    {
        if (!File.Exists(Application.persistentDataPath + "/HighScore.sav"))
        {
            highScore = 0;
            SaveScore();
        }

        else
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/HighScore.sav", FileMode.Open);
            HighScore hs = (HighScore)bf.Deserialize(file);
            file.Close();

            highScore = hs.highScore;
        }
    }

    public void UpdateHighScore(int value)
    {
        highScore = value;
    }

    public int GetHighScore()
    {
        return highScore;
    }
}
