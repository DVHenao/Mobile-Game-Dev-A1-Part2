/*
MainMenuUI.cs
Made by Emmanuelle Henao, Student Number: 101237746
Last Modified: October 19th, 2023
Game2014 - Mobile Dev
Revision History: added General UI Functionality for both in game and in menu UI - Oct 19th, 2023 
*/



using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{

    public GameObject GameOverUI;
    public GameObject player;

    public TMP_Text ScoreValue;
    public TMP_Text GameOverScoreValue;
    public int ScoreCount;


    public GameObject GameAudioPlayer;
    public GameObject GameOverAudioPlayer;
    public GameObject MainMenuAudioPlayer;

    public GameObject MobileJoystick;



    public void Start()
    {
        player = GameObject.FindWithTag("Player");


        bool usingMobileInput = Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer;

        if (!usingMobileInput)
        {
            Debug.Log("playing on PC");
            HideUI();
        }
        else { Debug.Log("playing on phone"); }
    }


    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void BackToMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void PlayAgain()// self explanatory
    {
        SceneManager.LoadScene(1);
        GameAudioPlayer.SetActive(true);
        GameOverAudioPlayer.SetActive(false);
    }


    public void TakeDamage()
    {
        if(player.GetComponent<PlayerScript>().currentHealth<=0)
        {
            GameOver();
        }
    }

    public void GameOver() // self explanatory
    {
        GameOverUI.SetActive(true);
        GameAudioPlayer.SetActive(false);
        GameOverAudioPlayer.SetActive(true);

        GameOverScoreValue.text = "Score: " + ScoreCount.ToString();
    }

    public void MoveScore()// self explanatory
    {
        ScoreCount += 10;
        ScoreValue.text = ScoreCount.ToString();

    }

    void HideUI()
    {
        MobileJoystick.SetActive(false);
    }

}
