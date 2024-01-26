using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InputComponent : MonoBehaviour
{
    //inputs
    MyInputs controls = null;
    InputAction move = null;
    InputAction leftClick = null;
    InputAction camRot = null;

    //Accessors
    public InputAction Move => move;
    public InputAction CamRot => camRot;
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
        camRot = controls.Player.camRot;
        camRot.Enable();

    
    }


}
