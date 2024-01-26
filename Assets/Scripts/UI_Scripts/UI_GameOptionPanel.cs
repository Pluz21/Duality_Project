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

    //
    void Start()
    {
        Init();
    }

    void Init()
    {
        
        gameOptionDropdown.onValueChanged.AddListener(PlayClickSound);
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
