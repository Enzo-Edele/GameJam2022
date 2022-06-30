using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Declaration
    public List<int> highScoreList = new List<int>();

    public int score;

    public Tower tower;
    public ProjectileSpawner spawner;
    public GameObject barrier;
    public GameObject powerUpPrefab;
    public int dropRate = 2;

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
                UIManager.Instance.DeactivateScore();
                //Debug.Log("InMenu");
                break;
            case GameStates.InGame:
                UIManager.Instance.ActivateScore();
                //Debug.Log("InGame");
                break;
            case GameStates.Pause:
                UIManager.Instance.ActivateScore();
                //Debug.Log("Pause");
                break;
            case GameStates.Credits:
                UIManager.Instance.DeactivateScore();
                //Debug.Log("Credits");
                break;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && gameState == GameStates.InGame)
        {
            UIManager.Instance.ActivatePauseMenu();
            ChangeGameState(GameStates.Pause);
        }
    }

    public void UpdateScore(int point, Vector2 scorePos)
    {
        score += point;
        UIManager.Instance.UpadateScore(point);
    }

    public void GameOver()
    {
        Debug.Log("Loose");
        UIManager.Instance.ActivateMenuGameOver();
        //active menuObject
    }
}
