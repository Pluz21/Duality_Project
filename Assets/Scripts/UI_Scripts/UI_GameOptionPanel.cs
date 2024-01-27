using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UI_GameOptionPanel : MonoBehaviour
{

    [SerializeField] TMP_Dropdown gameOptionDropdown = null;
    [SerializeField] AudioSource soundSource = null;
    [SerializeField] int difficultyScaling = 1;
    [SerializeField] int baseDifficulty = 1;

    //Manager
    [SerializeField] EnemyManager enemyManager = null;


    //
    void Start()
    {
        Init();
    }

    void Init()
    {
        enemyManager = FindAnyObjectByType<EnemyManager>();
        DropdownItemSelected(gameOptionDropdown);
        gameOptionDropdown.onValueChanged.AddListener(PlayClickSound);
        gameOptionDropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(gameOptionDropdown); });
    }

    private void DropdownItemSelected(TMP_Dropdown gameOptionDropdown)
    {
        int _index = gameOptionDropdown.value;
        int _size = enemyManager.AllEnemiesCount;
        Debug.Log($"current amount of ennemies {_size}");
        for (int i = 0; i < _size; i++)
        {
            enemyManager.AllEnemies[i].Damage = _index + baseDifficulty * difficultyScaling;
        
            enemyManager.AllEnemies[i].AttackSpeed = _index + baseDifficulty * difficultyScaling + enemyManager.AllEnemies[i].AttackSpeed;
            enemyManager.AllEnemies[i].EnemyMoveSpeed = _index + baseDifficulty * difficultyScaling + enemyManager.AllEnemies[i].EnemyMoveSpeed;

            //enemyManager.AllEnemies[i].
        }
        Debug.Log($"Now selected {_index}");


    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayClickSound(int _int)
    {
        soundSource.Play();
    }
 
}
