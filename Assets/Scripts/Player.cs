using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputComponent),typeof(MovementComponent),typeof(DetectionComponent))]
public class Player : MonoBehaviour
{
    MovementComponent movementComponent = null;
    InputComponent inputComponent = null;

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
