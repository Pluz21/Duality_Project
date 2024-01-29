using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfLevel : MonoBehaviour
{
    [SerializeField] Player playerRef = null;
    [SerializeField] GameObject winMenu = null;
    void Start()
    {
        Init();
    }

    void Init()
    {
        playerRef = FindAnyObjectByType<Player>();  
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.GetComponent<Player>()) return;
        Debug.Log("You have found your way out. Onto the next level!");
        winMenu.SetActive(true);
        Time.timeScale = 0f;
    }
    void Update()
    {
        
    }
}
