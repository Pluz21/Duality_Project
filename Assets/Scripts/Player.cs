using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputComponent),typeof(MovementComponent))]
public class Player : MonoBehaviour
{
    [SerializeField] int life = 50;
    [SerializeField] int maxLife = 50;
    MovementComponent movementComponent = null;
    InputComponent inputComponent = null;

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
