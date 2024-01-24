using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class MovementComponent : MonoBehaviour
{
    private InputComponent input = null;

    //Movement variables
    [SerializeField] float moveSpeed = 10;
    [SerializeField] float rotationSpeed = 50;
    [SerializeField] float DistDash = 5;

    [SerializeField] bool isCrouching = false;
    [SerializeField] bool isInvisible = false;

    [SerializeField] float timeInvi = 6;

    //Raycast elements
    [SerializeField] Vector3 worldPosition = Vector3.zero;
    [SerializeField] LayerMask floorMask = 0;
    [SerializeField] Ray screenRay = new Ray();
    [SerializeField] bool detectFloor = false;

    //Accessors
    public Vector3 WorldPosition => worldPosition;

    public bool IsCrouching => isCrouching;
    public bool IsInvisible => isInvisible;



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
        dash();
        Crouch();
        Invi();
        if(isInvisible)
            TimeInvi();
    }


    public void Move()
    {
        Vector3 _moveDirection = input.Move.ReadValue<Vector3>();

        if (_moveDirection.z > 0 || _moveDirection.z < 0)
            transform.position += transform.forward * _moveDirection.z * Time.deltaTime * moveSpeed;
        if (_moveDirection.x > 0 || _moveDirection.x < 0)
            transform.position += transform.right * _moveDirection.x * Time.deltaTime * moveSpeed;
    }
    public void Rotate()
    {
        float _rotationValue = input.CamRot.ReadValue<float>();
        transform.eulerAngles += transform.up * _rotationValue * rotationSpeed * Time.deltaTime;
    }  

    public void  dash()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isCrouching) return;
            transform.Translate(Vector3.forward * DistDash);
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
        if (timeInvi > 0)
        {
            isInvisible = true;
            timeInvi -= Time.deltaTime;
        }
        else
        {
            timeInvi = 0;
            isInvisible = false;
            timeInvi = 6;
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
