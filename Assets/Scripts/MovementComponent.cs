using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementComponent : MonoBehaviour
{
    private InputComponent input = null;
    [SerializeField] float moveSpeed = 10;

    void Start()
    {
        Init();
    }

    void Init()
    {
        input = GetComponent<InputComponent>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move()
    {
        Vector3 _moveDirection = input.Move.ReadValue<Vector3>();

        if(_moveDirection.z > 0 || _moveDirection.z< 0)
            transform.position += transform.forward* _moveDirection.z* Time.deltaTime* moveSpeed;
        if(_moveDirection.x > 0 || _moveDirection.x< 0)
            transform.position += transform.right* _moveDirection.x* Time.deltaTime* moveSpeed;
    }
}
