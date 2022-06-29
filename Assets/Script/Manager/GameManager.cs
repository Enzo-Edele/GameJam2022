using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using Cinemachine;
using System.IO;

public class GameManager : MonoBehaviour
{
    #region Declaration
    public int file;

    public List<int> highScoreList = new List<int>();

    public int maxLives;
    public int lives;
    public int score;

    public SpriteRenderer brick;
    public float brickWidth;
    public float brickHeight;

    #endregion

    public enum GameStates
    {
        InMenu,
        InGame,
        Pause,
        Credits
    }
    private static GameStates gameState;
    public static GameStates GameState;

    public static GameManager Instance { get; private set; }
    void Awake()
    {
        Instance = this;
        brickHeight = brick.bounds.size.y;
        brickWidth = brick.bounds.size.x;
        ChangeGameState(GameStates.InMenu);
    }

    public void ChangeGameState(GameStates currentState)
    {
        gameState = currentState;
        GameState = gameState;
        switch (gameState)
        {
            case GameStates.InMenu:
                //Debug.Log("InMenu");
                break;
            case GameStates.InGame:
                //Debug.Log("InGame");
                break;
            case GameStates.Pause:
                //Debug.Log("Pause");
                break;
            case GameStates.Credits:
                //Debug.Log("Credits");
                break;
        }
    }

    void Update()
    {
        
    }

    public void SetUpStartValue(int file)
    {
        this.file = file;
        for (int i = 0; i < 7; i++)
        {
            highScoreList.Add(0);
        }
    }
    public void Save(int file)
    {
        SaveSysteme.Save(this, file);
        //Debug.Log("save to file : " + file);
    }
    public void Load(int file)
    {
        SetUpStartValue(file);
        string path = Application.persistentDataPath + "/data" + file + ".save";
        if (File.Exists(path))
        {
            SaveData data = SaveSysteme.LoadData(file);
            this.file = data.file;
            for (int i = 0; i < 7; i++)
            {
                highScoreList[i] = (data.highScoreList[i]);
            }
            //Debug.Log("load file : "+ file);
        }
    }

    public void ChangeLife(int damage)
    {
        lives += damage;
        if (lives <= 0)
        {
            //faire mourrir
        }
        UIManager.Instance.UpdateLives();
    }
    public void UpdateScore(int point, int index)
    {
        score += point;
        UIManager.Instance.UpadateScore(point);
    }
}
