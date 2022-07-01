using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    #region Declaration
    [SerializeField] GameObject menu;
    [SerializeField] GameObject menuGameOver;
    [SerializeField] GameObject credits;

    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject optionMenu;

    [SerializeField] GameObject powerUpBoxes;
    [SerializeField] Image powerUpImage;
    [SerializeField] GameObject spaceBarIcon;
    public PowerUps powerUps;

    [SerializeField] GameObject score;
    [SerializeField] TMP_Text scoreText;
    //[SerializeField] Animator textAnimator;
    //[SerializeField] TMP_Text scoreAddText;
    List<string> music = new List<string>();
    int track;
    public string tampon;
    #endregion

    public static UIManager Instance { get; private set; }
    void Awake()
    {
        Instance = this;
        ActivateMenu();
        music.Add("PsyKick");
        music.Add("Acceleron");
        music.Add("AngryRobot");
    }

    void Update()
    {
        
    }

    public void ChooseMusic()
    {
        track = Random.Range(0, music.Count);
        SoundManager.Instance.PlayMusic(music[track]);
        tampon = music[track];

    }
    public void ActivateMenu() //main menu
    {
        menu.SetActive(true);
    }
    public void DeactivateMenu()
    {
        menu.SetActive(false);
    }
    
    public void ActivatePauseMenu() //pause menu
    {
        pauseMenu.SetActive(true);
    }
    public void DeactivatePauseMenu()
    {
        pauseMenu.SetActive(false);
    }
    public void ActivateOptionMenu() //option menu
    {
        optionMenu.SetActive(true);
    }
    public void DeactivateOptionMenu()
    {
        optionMenu.SetActive(false);
    }
    public void ActivateMenuGameOver() //gameOver menu
    {
        pauseMenu.SetActive(false);
        menuGameOver.SetActive(true);
    }
    public void DeactivateMenuGameOver()
    {
        menuGameOver.SetActive(false);
    }

    public void ActivateScore() //activate score
    {
        score.SetActive(true);
        scoreText.text = "Score : " + (0 + GameManager.Instance.score);
    }
    public void DeactivateScore()
    {
        score.SetActive(false);
    }
    public void UpadateScore(int point)
    {
        scoreText.text = "Score : " + GameManager.Instance.score;
        if (point > 0)
        {
            //textAnimator.SetTrigger("Add");
            //scoreAddText.text = "+" + point;
        }
    }
    public void ActivatePowerUpBox() //activate score
    {
        powerUpBoxes.SetActive(true);
    }
    public void DeactivatePowerUpBox()
    {
        powerUpBoxes.SetActive(false);
    }

    public void GetPowerUp(PowerUps power, Sprite sprite)
    {
        if (powerUps != null)
            Destroy(powerUps.gameObject);
        powerUps = power;
        powerUps.gameObject.transform.position = new Vector2(10, -7);
        powerUps.isSelect = true;
        spaceBarIcon.SetActive(true);
        powerUpImage.sprite = sprite;
    }
    public void UsePowerUp()
    {
        if (powerUps != null)
        {
            powerUps.Use();
            Destroy(powerUps.gameObject);
            powerUps = null;
        }
        spaceBarIcon.SetActive(false);
        powerUpImage.sprite = null;
    }
    void ResetPowerUps()
    {
        if (powerUps != null)
        {
            Destroy(powerUps.gameObject);
            powerUps = null;
            spaceBarIcon.SetActive(false);
            powerUpImage.sprite = null;
        }
    }

    public void ButtonStart() //start the game
    {
        DeactivateMenu();
        ActivatePowerUpBox();
        ResetPowerUps();
        ChooseMusic();
        GameManager.Instance.spawner.GetComponent<ProjectileSpawner>().DestroyAll();
        GameManager.Instance.barrier.GetComponent<Barrier>().ChangeLife(-3);
        SoundManager.Instance.Play("GameStart");
        GameManager.Instance.ChangeGameState(GameManager.GameStates.InGame);
        GameManager.Instance.tower.BuildTower();
        GameManager.Instance.UpdateScore(-GameManager.Instance.score);
    }
    //button option
    public void ButtonResume() //exit pause and resume game
    {
        SoundManager.Instance.UnpauseMusic(tampon);
        DeactivatePauseMenu();
        Time.timeScale = 1f;
        SoundManager.Instance.Play("GameStart");
        GameManager.Instance.ChangeGameState(GameManager.GameStates.InGame);
        //resume movement and spawn
    }
    public void ButtonRetry() //restart game from game over
    {
        DeactivateMenuGameOver();
        DeactivatePauseMenu();
        ResetPowerUps();
        GameManager.Instance.spawner.GetComponent<ProjectileSpawner>().DestroyAll();
        GameManager.Instance.barrier.GetComponent<Barrier>().ChangeLife(-3);
        Time.timeScale = 1f;
        GameManager.Instance.tower.BuildTower();
        GameManager.Instance.UpdateScore(-GameManager.Instance.score);
        SoundManager.Instance.Play("GameStart");
        GameManager.Instance.ChangeGameState(GameManager.GameStates.InGame);
    }
    public void ButtonStartMenu() //go back to start menu from game over
    {
        DeactivateMenuGameOver();
        ActivateMenu();
        SoundManager.Instance.Play("Button");
    }
    public void ButtonCredit() //launch credit
    {
        SoundManager.Instance.Play("Button");
        GameManager.Instance.ChangeGameState(GameManager.GameStates.Credits);
        credits.SetActive(true);
    }
    public void ButtonQuit() //exit app
    {
        SoundManager.Instance.Play("Button");
        Application.Quit();
    }

    public void SliderHolder(float holdValue)
    {
        //GameManager.Instance.holdValue = holdValue;
    }
}
