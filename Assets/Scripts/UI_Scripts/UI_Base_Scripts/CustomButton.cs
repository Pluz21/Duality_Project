using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomButton : MonoBehaviour 
{
    public event Action OnButtonClick;
    public event Action OnButtonHover;


    [SerializeField] AudioSource onHoverSoundSource = null;
    [SerializeField] AudioSource onClickSoundSource = null;
    [SerializeField] Button button = null;

    private void OnMouseEnter()
    {
        ExecuteHover();
        Debug.Log("nouse entered");
    }
    void Start()
    {
        Init();
    }
    void Init()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ExecuteClick);
        OnButtonClick += ManageOnClick;
        OnButtonHover += ManageOnHover;
        
        
        
            onClickSoundSource.playOnAwake = false;  
            onHoverSoundSource.playOnAwake = false;  

    }
   

    protected virtual void ManageOnClick()
    {
        onClickSoundSource.Play();
    }
    protected virtual void ManageOnHover()
    { 
        onHoverSoundSource.Play();
        
    }
    protected virtual void ExecuteClick()
    {
        OnButtonClick?.Invoke();
    }
    protected virtual void ExecuteHover()
    {
        OnButtonHover?.Invoke();

    }
}
