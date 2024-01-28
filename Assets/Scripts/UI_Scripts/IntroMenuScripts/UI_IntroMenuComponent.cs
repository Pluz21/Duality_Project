using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_IntroMenuComponent : MonoBehaviour
{
    [SerializeField] Button startGameButton = null;
    [SerializeField] Button quitGameButton = null;
    [SerializeField] Scene sceneToStart;
    [SerializeField] string  sceneToStartString = "TimCityScene";
    void Start()
    {
        Init();
    }
    void Init()
    {
        startGameButton.onClick.AddListener(StartLevel);
        quitGameButton.onClick.AddListener(QuitGame);
    }

    private void StartLevel()
    {
       
        SceneManager.GetSceneByName(sceneToStartString);
        
        SceneManager.LoadScene(sceneToStartString, LoadSceneMode.Single);   

    }
    private void QuitGame()
    {
        //UnityEditor.EditorApplication.isPlaying = false; DEV ONLY
        Application.Quit();
    }

    void Update()
    {
        
    }
}
