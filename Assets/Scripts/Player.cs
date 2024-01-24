using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputComponent),typeof(MovementComponent))]
public class Player : MonoBehaviour
{
    [SerializeField] float detectionRange = 10;
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
    // Update is called once per frame
    void Update()
    {
        movementComponent.Move();
    }

    private void OnDrawGizmos()
    {
        AnmaGizmos.DrawSphere(transform.position, detectionRange, Color.blue);
    }
}
