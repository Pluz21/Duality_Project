using UnityEngine.UI;

using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_WinMenu : MonoBehaviour
{
    [SerializeField] GameObject winMenuPanel = null;
    [SerializeField] Button restartButton = null;
    [SerializeField] Scene sceneToRestart;
    void Start()
    {
        restartButton = GetComponentInChildren<Button>();
        winMenuPanel.SetActive(false);
        if(restartButton)
        restartButton.onClick.AddListener(RestartLevel);
    }

    public void RestartLevel()
    {
        if (!sceneToRestart.isLoaded)
        {
            sceneToRestart = SceneManager.GetActiveScene();
        }
        SceneManager.LoadScene(sceneToRestart.name, LoadSceneMode.Single);
        Time.timeScale = 1;

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
