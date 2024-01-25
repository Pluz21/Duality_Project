using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class MovementComponent : MonoBehaviour
{
    private InputComponent input = null;
    [SerializeField] Player playerRef = null;


    //Movement variables
    [SerializeField] float moveSpeed = 10;
    [SerializeField] float rotationSpeed = 50;
    [SerializeField] float DistDash = 5;
    [SerializeField] int numberDash = 3;
    [SerializeField] float regenDash = 5;

    [SerializeField] bool isCrouching = false;
    [SerializeField] bool isInvisible = false;

    [SerializeField] float timerInvi = 6;

    //Raycast elements
    [SerializeField] Vector3 worldPosition = Vector3.zero;
    [SerializeField] LayerMask floorMask = 0;
    [SerializeField] Ray screenRay = new Ray();
    [SerializeField] bool detectFloor = false;

    //event anim
    public event Action<float> OnforwardAxis = null;
    public event Action<float> OnRightAxis = null;
    public event Action<float> OnRotationAxis = null;

    

    //Accessors
    public Vector3 WorldPosition => worldPosition;
    public bool IsCrouching => isCrouching;
    public bool IsInvisible => isInvisible;
    public int NumberDash => numberDash;
    public float RegenDash
    {
        get { return regenDash; }
        set { regenDash = value; }
    }
    public float TimerInvi
    {
        get { return timerInvi; }
        set { timerInvi = value; }
    }



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
        Move();
        Rotate();  
        Dash();
        Crouch();
        Invi();
        Timedash();
        if (isInvisible)
            TimeInvi();

       
    }


    public void Move()
    {
        Vector3 _moveDirection = input.Move.ReadValue<Vector3>();

        if (playerRef == null) return;

        transform.position += transform.forward * _moveDirection.z * Time.deltaTime * moveSpeed;
        OnforwardAxis?.Invoke(_moveDirection.z);
        transform.position += transform.right * _moveDirection.x * Time.deltaTime * moveSpeed;
        OnRightAxis?.Invoke(_moveDirection.x);
        
    }
    public void Rotate()
    {
        float _rotationValue = input.CamRot.ReadValue<float>();
        transform.eulerAngles += transform.up * _rotationValue * rotationSpeed * Time.deltaTime;
        OnRotationAxis?.Invoke(_rotationValue);
    }  

    public void  Dash()
    {
        if (Input.GetKeyDown(KeyCode.Space) && numberDash >= 1)
        {
            if (isCrouching) return;
            transform.Translate(Vector3.forward * DistDash);
            numberDash--;
        }

    }

    public void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if(isCrouching == true)
            {
                moveSpeed = 10;
                isCrouching = false;
                //Todo Anim 
            }

            else
            {
                moveSpeed = 3;
                isCrouching = true;
                //Todo Anim 
            }
        }
    }

    public void Invi()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
           TimeInvi();
          
        }
    }
    public void TimeInvi()
    {

        if (timerInvi > 0)
        {
            isInvisible = true;
            timerInvi -= Time.deltaTime;
        }
        else
        {
           
            timerInvi = 0;
            isInvisible = false;
            timerInvi = 6;
        }
        
    }
    public void Timedash()
    {
        if(numberDash <= 2 && regenDash >0)
        {
            regenDash -= Time.deltaTime;
            if (regenDash <= 0)
                numberDash++;
        }
        else
        {
            regenDash = 0;
            regenDash = 8;
        }
        
    }
    


    public void ClickToMove()
    {

    }

    void DetectFloor()
    {
        //Vector2 _pos2D = input.ClickToMove.ReadValue<Vector2>();
        //Vector3 _pos = new Vector3(_pos2D.x, _pos2D.y, detectionDistance);
        //screenRay = Camera.main.ScreenPointToRay(_pos);
        ////screenRay = new Ray (Camera.main.transform.position, Camera.main.transform.forward);

        //bool _hitWall = Physics.Raycast(screenRay, out RaycastHit _result, detectionDistance, maskWall);
        //detect = _hitWall;
        //if (!_hitWall) return;

        //OnWallHit?.Invoke(_result);
        //OnWallHitPosition?.Invoke(_result.point);

        //if (_result.transform.gameObject == gameObjectHit) return;
        //OnWallHitGameObject?.Invoke(_result.transform.gameObject);
        //allGameObjectsHit.Add(_result.transform.gameObject);
        //int _size = allGameObjectsHit.Count;
        //if (_size <= 0) return;
        //for (int i = _size - 1; i >= 0; i--)
        //{
        //    if (_result.transform.gameObject != allGameObjectsHit[i])
        //    {
        //        RevealGameObject(allGameObjectsHit[i]);
        //        allGameObjectsHit.RemoveAt(i);
        //    }
        //}
    }
}
