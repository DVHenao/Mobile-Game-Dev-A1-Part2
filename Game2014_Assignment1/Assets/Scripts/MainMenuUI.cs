/*
MainMenuUI.cs
Made by Emmanuelle Henao, Student Number: 101237746
Last Modified: October 2nd, 2023
Game2014 - Mobile Dev
Revision History: added General UI Functionality - Oct 2nd, 2023 
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

    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void BackToMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(1);
    }


    public void TakeDamage()
    {
        Destroy(Hearts[Hearts.Length - 1]);

       if (Hearts.Length > 0)
       System.Array.Resize(ref Hearts,Hearts.Length-1);

       if (Hearts.Length == 0)
            GameOver();

    }

    public void GameOver()
    {
        GameOverUI.SetActive(true);
    }

    public void MoveScore()
    {
        ScoreCount += 10;
        ScoreValue.text = ScoreCount.ToString();

    }
}
