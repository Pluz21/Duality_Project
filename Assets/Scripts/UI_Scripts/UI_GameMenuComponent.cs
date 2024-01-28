using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class UI_GameMenuComponent : MonoBehaviour
{
    //inputs
    MyInputs controls = null;
    InputAction openMenu = null;

    //UI variables
    [SerializeField] GameObject gameMenu = null;
    [SerializeField] GameObject optionPanel = null;
    [SerializeField] Button resumeButton = null;
    [SerializeField] Button restartButton = null;
    [SerializeField] Button optionsButton = null;
    [SerializeField] Button quitButton = null;

    //Restart variables
    [SerializeField] Scene sceneToRestart;

    // Init Sound
    [SerializeField] buttonSoundComponent buttonSoundCompo = null;


  

    private void Awake()
    {
        controls = new MyInputs();
    }
    void Start()
    {
        Init();
        buttonSoundCompo.Init();
        gameMenu.SetActive(false);
        optionPanel.SetActive(false);
    }

    void Init()
    {
        //resumeButton = gameMenu.GetComponentInChildren<Button>();
        openMenu.performed += OpenGameMenu;
        resumeButton.onClick.AddListener(ResumeGame);
        restartButton.onClick.AddListener(RestartGame);
        optionsButton.onClick.AddListener(OpenOptionsPanel);
        quitButton.onClick.AddListener(QuitGame);
        buttonSoundCompo = GetComponent<buttonSoundComponent>();
        


    }



    private void OpenOptionsPanel()
    {
        optionPanel.SetActive(true);
        optionsButton.onClick.AddListener(CloseOptionsPanel);
    }
    private void CloseOptionsPanel()
    {
        optionPanel.SetActive(false);
        optionsButton.onClick.AddListener(OpenOptionsPanel);

    }

    private void RestartGame()
    {
        if (!sceneToRestart.isLoaded)
        {
            sceneToRestart = SceneManager.GetActiveScene();
        }
        SceneManager.LoadScene(sceneToRestart.name,LoadSceneMode.Single);
        ResumeGame();
    }

    private void QuitGame()
    {
        //UnityEditor.EditorApplication.isPlaying = false; DEV ONLY
        Application.Quit();
    }

    void Update()
    {

    }

    void OpenGameMenu(InputAction.CallbackContext _context)
    {
        Debug.Log("opening menu");
        openMenu.performed -= OpenGameMenu;
        openMenu.performed += CloseGameMenu;
            PauseGame();

    }
    void CloseGameMenu(InputAction.CallbackContext _context)
    {
        openMenu.performed += OpenGameMenu;
        openMenu.performed -= CloseGameMenu;
        ResumeGame();

    }

    void PauseGame()
    {
        Time.timeScale = 0;
        if (!gameMenu) return;
        gameMenu.gameObject.SetActive(true);


    }
    void ResumeGame()
    {
        Time.timeScale = 1;
        if (!gameMenu) return;
        gameMenu.gameObject.SetActive(false);

    }


    private void OnEnable()
    { 
        // UI

        openMenu = controls.GameMenu.MenuInteract;

        openMenu.Enable();
    }


}
