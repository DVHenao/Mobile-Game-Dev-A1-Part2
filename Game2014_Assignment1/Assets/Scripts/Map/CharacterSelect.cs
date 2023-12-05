using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelect : MonoBehaviour
{
    bool characterSelected;
    public PlayerScriptableObject characterData;
    public GameObject player;


    public void Start()
    {
        player = GameObject.FindWithTag("Player");
    }
    // Update is called once per frame
    void Update()
    {
     if (!characterSelected) 
        { PauseGame(); }   
    }


    void PauseGame() // self  explantory
    {
       if(Time.timeScale != 0)
        Time.timeScale = 0;
    }
    void ResumeGame() // self explantory
    {
        if (Time.timeScale != 1)
            Time.timeScale = 1;
    }

    public void SelectCharacter(PlayerScriptableObject character)
    {
        characterData = character;
        player.GetComponent<PlayerScript>().SetStartingPlayer(characterData);
        player.GetComponent<PlayerScript>().SpawnWeapon(characterData.StartingWeapon);
        player.GetComponent<PlayerScript>().SpawnWeapon(player.GetComponent<PlayerScript>().secondWeaponTest);
        characterSelected = true;
        ResumeGame();
        gameObject.SetActive(false);
    }

}
