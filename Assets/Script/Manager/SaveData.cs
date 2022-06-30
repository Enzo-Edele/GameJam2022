using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int file;
    public int score;
    public int[] highScoreList = new int[GameManager.Instance.highScoreList.Count];


    public SaveData(GameManager gameData)
    {
        //file = gameData.file;
        score = gameData.score;
        for (int i = 0; i < gameData.highScoreList.Count; i++)
        {
            highScoreList[i] = gameData.highScoreList[i];
        }
    }
}
