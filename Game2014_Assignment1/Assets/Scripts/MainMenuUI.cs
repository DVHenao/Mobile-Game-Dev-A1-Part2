/*
MainMenuUI.cs
Made by Emmanuelle Henao, Student Number: 101237746
Last Modified: October 2nd, 2023
Game2014 - Mobile Dev
Revision History: added General UI Functionality - Oct 2nd, 2023 
*/



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
  
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void BackToMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }




}
