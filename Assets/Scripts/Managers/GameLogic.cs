using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    [SerializeField] Player playerRef = null;
    [SerializeField] UI_GameMenuComponent gameMenuCompo = null;
    [SerializeField] GameObject deathMenu = null;

    void Start()
    {
        Init();
    }

    void Init()
    { 
        playerRef = FindAnyObjectByType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerRef.transform.position.y < -100)
        {
            gameMenuCompo.OpenGameMenu();
            deathMenu.SetActive(true);
        }

    }

  
}
