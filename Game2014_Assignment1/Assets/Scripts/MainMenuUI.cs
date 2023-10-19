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

    public GameObject[] Hearts;
    public GameObject GameOverUI;

    public TMP_Text ScoreValue;
    public int ScoreCount;


    public GameObject GameAudioPlayer;
    public GameObject GameOverAudioPlayer;
    public GameObject MainMenuAudioPlayer;

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
        Destroy(Hearts[Hearts.Length - 1]);

       if (Hearts.Length > 0)
       System.Array.Resize(ref Hearts,Hearts.Length-1);// self explanatory we do this to dynamically change the array, could use list instead

        if (Hearts.Length == 0)
            GameOver();

    }

    public void GameOver() // self explanatory
    {
        GameOverUI.SetActive(true);
        GameAudioPlayer.SetActive(false);
        GameOverAudioPlayer.SetActive(true);
    }

    public void MoveScore()// self explanatory
    {
        ScoreCount += 10;
        ScoreValue.text = ScoreCount.ToString();

    }
}
