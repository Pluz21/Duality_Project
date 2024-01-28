using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_DeathMenu : MonoBehaviour
{
    [SerializeField] GameObject deathMenu = null;
    [SerializeField] ScrollRect kanjiScrollViewRect = null;
    [SerializeField] public float scrollSpeed = 1f;
    [SerializeField] Button restartButton = null;
    [SerializeField] Scene sceneToRestart;
    public GameObject DeathMenu => deathMenu;
    void Start()
    {
        Init();
    }

    public void Init()
    {
        deathMenu.SetActive(false);
        restartButton.onClick.AddListener(RestartLevel);




    }


    // Update is called once per frame
    void Update()
    {
        //if (deathMenu.activeSelf)
        //{
        //    //Not working auto scroll
        //    //float currentVerticalPosition = kanjiScrollViewRect.verticalNormalizedPosition;

        //    //float newVerticalPosition = currentVerticalPosition - Time.deltaTime * scrollSpeed;

        //    //newVerticalPosition = Mathf.Clamp01(newVerticalPosition);

        //    //kanjiScrollViewRect.verticalNormalizedPosition = newVerticalPosition;
        //}

    }
    public void RestartLevel()
    {
        Debug.Log("restarting");
        //if (!sceneToRestart.isLoaded)
        {
            sceneToRestart = SceneManager.GetActiveScene();
        }
        SceneManager.LoadScene(sceneToRestart.name, LoadSceneMode.Single);
        deathMenu.SetActive(false);
        Time.timeScale = 1;

    }
}
