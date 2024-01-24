using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputComponent : MonoBehaviour
{
    MyInputs controls = null;
    InputAction move = null;
    InputAction leftClick = null;
    InputAction mousePos = null;

    public InputAction Move => move;
    public InputAction LeftClick => leftClick;

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
        leftClick = controls.Player.LeftClickMove;
        leftClick.Enable();
    }


}
