using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputComponent : MonoBehaviour
{
    MyInputs controls = null;
    InputAction move = null;

    public InputAction Move => move;
    private void Awake()
    {
        controls = new MyInputs();
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    private void OnEnable()
    {
        move = controls.Player.Move;
        move.Enable();
    }


}
