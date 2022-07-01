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
    public List<Sprite> brick1Sprite;
    public List<Sprite> brick2Sprite;
    public List<Sprite> powerUpSprite;
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
                UIManager.Instance.DeactivatePowerUpBox();
                SoundManager.Instance.StopAllMusic();
                SoundManager.Instance.PlayMusic("Acceuil");
                //Debug.Log("InMenu");
                break;
            case GameStates.InGame:
                UIManager.Instance.ActivateScore();
                UIManager.Instance.ActivatePowerUpBox();
                SoundManager.Instance.PauseMusic("Acceuil");
                //Debug.Log("InGame");
                break;
            case GameStates.Pause:
                UIManager.Instance.ActivateScore();
                UIManager.Instance.ActivatePowerUpBox();
                SoundManager.Instance.UnpauseMusic("Acceuil");
                SoundManager.Instance.PauseMusic(UIManager.Instance.tampon);
                //Debug.Log("Pause");
                break;
            case GameStates.Credits:
                UIManager.Instance.DeactivateScore();
                UIManager.Instance.DeactivatePowerUpBox();
                //Debug.Log("Credits");
                break;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && gameState == GameStates.InGame)
        {
            UIManager.Instance.ActivatePauseMenu();
            Time.timeScale = 0f;
            ChangeGameState(GameStates.Pause);
        }
        if (Input.GetKeyDown(KeyCode.Space) && gameState == GameStates.InGame)
        {
            UIManager.Instance.UsePowerUp();
        }
    }

    public void UpdateScore(int point)
    {
        score += point;
        UIManager.Instance.UpadateScore(point);
    }

    public void GameOver()
    {
        UIManager.Instance.ActivateMenuGameOver();
        ChangeGameState(GameStates.InMenu);
    }
}
