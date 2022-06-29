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

    
    [SerializeField] GameObject score;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] Animator textAnimator;
    [SerializeField] TMP_Text scoreAddText;
    #endregion

    public static UIManager Instance { get; private set; }
    void Awake()
    {
        Instance = this;
        ActivateMenu();
    }

    void Update()
    {
        
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
        /*if (point > 0)
        {
            textAnimator.SetTrigger("add");
            scoreAddText.text = "+" + point;
        }*/
    }

    

    public void ButtonStart() //start the game
    {
        DeactivateMenu();
        SoundManager.Instance.Play("Button");
        GameManager.Instance.ChangeGameState(GameManager.GameStates.InMenu);
        //active gameObject
        //deactive menuObject
    }
    //button option
    public void ButtonResume() //exit pause and resume game
    {
        DeactivatePauseMenu();
        SoundManager.Instance.Play("Button");
        GameManager.Instance.ChangeGameState(GameManager.GameStates.InGame);
        //resume movement and spawn
    }
    public void ButtonRetry() //restart game from game over
    {
        DeactivateMenuGameOver();
        SoundManager.Instance.Play("Button");
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
