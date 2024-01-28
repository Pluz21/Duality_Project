using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(MovementComponent))]
public class Player : MonoBehaviour
{
    public event Action OnLifeTotalReachesZero;
    [SerializeField] int life = 50;
    [SerializeField] int maxLife = 50;
    [SerializeField]MovementComponent movementComponent = null;
    [SerializeField] PlayerAnimation playerAnimation = null;
    [SerializeField] InputComponent inputComponent = null;
    [SerializeField] UI_DeathMenu deathMenu = null;

    public MovementComponent MoveCompoRef => movementComponent;

    public int MaxLife
    {
        get { return maxLife; }
        set { maxLife = value; }
    }
    public int Life
    {
        get { return life; }
        set { life = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    void Init()
    {
        Time.timeScale = 1.0f;
        movementComponent = GetComponent<MovementComponent>();
        inputComponent = GetComponent<InputComponent>();
        playerAnimation = GetComponent<PlayerAnimation>();
        InitEvents();
        life = maxLife;
       

        
    }

    void InitEvents()
    {
        movementComponent.OnforwardAxis += playerAnimation.UpdateForwardAnimatorParam;
        movementComponent.OnRightAxis += playerAnimation.UpdateRightAnimatorParam;
        movementComponent.OnRotationAxis += playerAnimation.UpdateRotateAnimatorParam;
        movementComponent.dash += playerAnimation.UpdateIsDashingAnimatorParam;
        movementComponent.invi += playerAnimation.UpdateIsInvisibleAnimatorParam;
        OnLifeTotalReachesZero += DisplayDeathMenu;
        //inputComponent.LeftClick.performed += 
    }
    // Update is called once per frame
    void Update()
    {
        movementComponent.Move();
        if (life <= 0)
        {
        Debug.Log($"life {life}");
            life = 0;
            OnLifeTotalReachesZero?.Invoke();


        }
    }
    void DisplayDeathMenu()
    {
        Debug.Log("DeathMenucalled");
        deathMenu.gameObject.SetActive(true);
        Time.timeScale = 0;
    }


}
