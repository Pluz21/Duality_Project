using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementComponent))]
public class Player : MonoBehaviour
{
    [SerializeField] int life = 50;
    [SerializeField] int maxLife = 50;
    MovementComponent movementComponent = null;
    [SerializeField] PlayerAnimation playerAnimation = null;
    [SerializeField] InputComponent inputComponent = null;



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
        movementComponent = GetComponent<MovementComponent>();
        inputComponent = GetComponent<InputComponent>();
        playerAnimation = GetComponent<PlayerAnimation>();
        movementComponent.OnforwardAxis += playerAnimation.UpdateForwardAnimatorParam;
        movementComponent.OnRightAxis += playerAnimation.UpdateRightAnimatorParam;
        movementComponent.OnRotationAxis += playerAnimation.UpdateRotateAnimatorParam;
        movementComponent.invi += playerAnimation.UpdateInviAnimatorParam;
        movementComponent.dash += playerAnimation.UpdateDashAnimatorParam;

    }

    void InitEvents()
    { 
        //inputComponent.LeftClick.performed += 
    }
    // Update is called once per frame
    void Update()
    {
        movementComponent.Move();
    }

   
}
