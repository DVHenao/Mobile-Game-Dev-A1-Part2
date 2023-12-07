using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        Gameplay,
        Paused,
        GameOver,
        LevelUp
    }

    public GameState currentState;

    public GameState previousState;

    [Header("UI")]
    public GameObject pauseScreen;
    public GameObject levelUpScreen;


    public bool choosingUpgrade = false;

    public GameObject player;

    private void Awake()
    {
        DisableScreens();
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        switch (currentState)
        {
            case GameState.Gameplay:
                CheckForPauseAndResume();
                break;

            case GameState.Paused:
                CheckForPauseAndResume();
                break;

            case GameState.GameOver:
                break;

            case GameState.LevelUp:
                if(!choosingUpgrade)
                {
                    choosingUpgrade = true;
                    Time.timeScale = 0;
                    Debug.Log("upgradescreen");
                    levelUpScreen.SetActive(true);
                }
                break;

            default:
                Debug.LogWarning("state does not exist");
                break;
        }
    }


    public void ChangeState(GameState newState)
    {
        currentState = newState;
    }
    public void PauseGame()
    {

        if (currentState != GameState.Paused)
        {
            previousState = currentState;
            ChangeState(GameState.Paused);
            Time.timeScale = 0;
            pauseScreen.SetActive(true);
            Debug.Log("paused");
        }
    }
    public void ResumeGame()
    {
        if (currentState == GameState.Paused)
        {
            ChangeState(previousState);
            Time.timeScale = 1;
            pauseScreen.SetActive(false);
            Debug.Log("resumed");
        }
    }

    void CheckForPauseAndResume()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(currentState == GameState.Paused) 
            {
                ResumeGame();
            }
            else 
            { 
                PauseGame();
            }
        }
    }

    void DisableScreens()
    {
        pauseScreen.SetActive(false);
        levelUpScreen.SetActive(false);
    }

    public void StartLevelUp()
    {
        ChangeState(GameState.LevelUp);
        player.SendMessage("RemoveAndApplyUpgrades");
    }

    public void EndLevelUp()
    {
        ChangeState(GameState.LevelUp);
        Time.timeScale = 1f;
        levelUpScreen.SetActive(false);
        ChangeState(GameState.Gameplay);
    }


}