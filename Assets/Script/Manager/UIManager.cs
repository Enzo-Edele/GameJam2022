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
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject saveMenu;
    [SerializeField] Text saveText;
    [SerializeField] GameObject newGameButton;
    [SerializeField] GameObject loadButton;
    [SerializeField] List<Button> loadButtons;
    [SerializeField] GameObject levelMenu;
    [SerializeField] List<Button> LevelButtons;
    [SerializeField] List<Text> highScores;
    [SerializeField] List<Vector3> starRequirement;
    [SerializeField] List<Image> starLvl;
    [SerializeField] List<Sprite> starImage;
    [SerializeField] GameObject endLevel;
    [SerializeField] GameObject endLevelButtons;
    [SerializeField] GameObject retryButton;
    [SerializeField] GameObject endLevelNextLevelButton;
    [SerializeField] GameObject endLevelButtonLevel;
    [SerializeField] GameObject endLevelButtonMain;
    [SerializeField] GameObject endLevelButtonCredits;

    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject optionMenu;

    [SerializeField] GameObject icon;
    [SerializeField] Text IconText;  //ou TMP_Text
    [SerializeField] GameObject chrono;
    [SerializeField] TMP_Text chronometerText;
    [SerializeField] GameObject score;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] Animator textAnimator;
    [SerializeField] TMP_Text scoreAddText;
    [SerializeField] GameObject LivesDisplay;
    [SerializeField] List<Image> lives;
    [SerializeField] Sprite live, emptylive;
    #endregion

    public static UIManager Instance { get; private set; }
    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        
    }

    public void ActivateMainMenu() //main menu
    {
        mainMenu.SetActive(true);
    }
    public void DeactivateMainMenu()
    {
        if (mainMenu != null)
            mainMenu.SetActive(false);
    }
    public void ActivateNewGameMenu() //menu new game
    {
        saveMenu.SetActive(true);
        saveText.text = "New Game";
        newGameButton.SetActive(true);
    }
    public void ActivateSaveMenu() //load menu
    {
        CheckSaveExist();
        saveMenu.SetActive(true);
        saveText.text = "Load";
        loadButton.SetActive(true);
    }
    public void DeactivateNewGameSaveMenu()
    {
        saveMenu.SetActive(false);
        saveText.text = "";
        if (newGameButton != null)
            newGameButton.SetActive(false);
        if (loadButton != null)
            loadButton.SetActive(false);
    }
    public void ActivateLevelMenu() //slect level menu
    {
        levelMenu.SetActive(true);
        ExitCampain();
        CheckCampainProgress();
    }
    public void DeactivateLevelMenu()
    {
        levelMenu.SetActive(false);
    }
    public void ActivatePauseMenu() //pause menu
    {
        pauseMenu.SetActive(true);
    }
    public void DeactivatePauseMenu()
    {
        pauseMenu.SetActive(false);
    }
    public void ActivateOptionMenu() //pause menu
    {
        optionMenu.SetActive(true);
    }
    public void DeactivateOptionMenu()
    {
        optionMenu.SetActive(false);
    }
    public void ActivateEndLevel()
    {
        DeactivatePauseMenu();
        DeactivateOptionMenu();
        endLevel.SetActive(true);
    }
    public void DeactivateEndLevel()
    {
        endLevel.SetActive(false);
    }

    //icon fct goes here

    public void DactivateIcons()
    {
        icon.SetActive(false);
    }

    public void ActivateLives() //activate livesDisplay
    {
        LivesDisplay.SetActive(true);
        UpdateLives();
    }
    public void DeactivateLives()
    {
        LivesDisplay.SetActive(false);
    }
    public void UpdateLives()
    {
        for (int i = 0; i < GameManager.Instance.maxLives; i++)
        {
            if (GameManager.Instance.lives > i)
                lives[i].sprite = live;
            else
                lives[i].sprite = emptylive;
        }
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
        //scoreText.text = "Score : " + GameManager.Instance.score;
        /*if (point > 0)
        {
            textAnimator.SetTrigger("add");
            scoreAddText.text = "+" + point;
        }*/
    }

    public void CheckSaveExist() //make button for load interractible if the file already exists
    {
        for (int i = 0; i < loadButtons.Count; i++)
        {
            loadButtons[i].interactable = false;
            string path = Application.persistentDataPath + "/data" + (i + 1) + ".save";
            if (File.Exists(path))
                loadButtons[i].interactable = true;
        }
    }
    public void CheckCampainProgress()
    {
        for (int i = 0; i < LevelButtons.Count; i++) //adapt 
        {/*
            if (i < GameManager.Instance.levelUnlock)
                LevelButtons[i].interactable = true;
            highScores[i].text = GameManager.Instance.highScoreList[i].ToString() + " Points";
            //if statement to activate star
            if (GameManager.Instance.highScoreList[i] > starRequirement[i].z)
                starLvl[i].sprite = starImage[3];
            else if (GameManager.Instance.highScoreList[i] > starRequirement[i].y)
                starLvl[i].sprite = starImage[2];
            else if (GameManager.Instance.highScoreList[i] > starRequirement[i].x)
                starLvl[i].sprite = starImage[1];
            else
                starLvl[i].sprite = starImage[0];*/
        }
    }
    void ExitCampain()
    {
        for (int i = 0; i < LevelButtons.Count; i++)
        {
            LevelButtons[i].interactable = false;
            highScores[i].text = GameManager.Instance.highScoreList[i].ToString();
        }
    }

    public void ButtonNewGameMenu()//main menu -> new game menu
    {
        DeactivateMainMenu();
        ActivateNewGameMenu();
        SoundManager.Instance.Play("Button");
    }
    public void ButtonSaveMenu()//main menu/end level -> load menu
    {
        DeactivateMainMenu();
        DeactivateEndLevel();
        ActivateSaveMenu();
        SoundManager.Instance.Play("Button");
    }
    public void ButtonStartNewGame(int file)//new game menu -> level select + create save to file X
    {
        DeactivateNewGameSaveMenu();
        //GameManager.Instance.SetUpStartValue(file);
        //GameManager.Instance.Save(file);
        ActivateLevelMenu();
        SoundManager.Instance.Play("Button");
    }
    public void ButtonLevelMenu(int file)//load menu -> level select menu of file X OR tuto if tuto not completed
    {
        //GameManager.Instance.Load(file);
        DeactivateNewGameSaveMenu();
        ActivateLevelMenu();
        SoundManager.Instance.Play("Button");
    }
    public void ButtonSelectLevel(string level)//level select menu -> level X
    {
        DeactivateLevelMenu();
        SceneManager.LoadScene(level);
        SoundManager.Instance.Play("Button");
        //GameManager.Instance.ChangeGameState(GameManager.GameStates.InGame);
    }
    public void ButtonResume()//pause menu -> level
    {
        DeactivatePauseMenu();
        SoundManager.Instance.Play("Button");
        //GameManager.Instance.ChangeGameState(GameManager.GameStates.InGame);
    }
    //button next lvl
    public void ButtonRetry()
    {
        DeactivateEndLevel();
        SoundManager.Instance.Play("Button");
        //GameManager.Instance.ChangeGameState(GameManager.GameStates.InGame);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ButtonNextLevel()//level -> level + 1
    {
        DeactivateEndLevel();
        SoundManager.Instance.Play("Button");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //GameManager.Instance.ChangeGameState(GameManager.GameStates.InGame);
    }
    public void ButtonQuit()//exit app
    {
        Application.Quit();
    }

    public void SliderHolder(float holdValue)
    {
        //GameManager.Instance.holdValue = holdValue;
    }
}
