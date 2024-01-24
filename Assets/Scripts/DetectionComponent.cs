using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class DetectionComponent : MonoBehaviour
{
    public event Action<GameObject> OnAggro = null;
    [SerializeField] GameObject target = null;
    [SerializeField] Enemy enemyOwner = null;

    // Raycast variables
    [SerializeField] float detectionRange = 10;
    [SerializeField] RaycastHit hitPlayer;
    [SerializeField] GameObject rayCenterStart = null;
    [SerializeField] GameObject rayRightStart = null;
    [SerializeField] GameObject rayLeftStart = null;
    [SerializeField] Ray centerRay;
    [SerializeField] Ray rightRay;
    [SerializeField] Ray leftRay;
    [SerializeField] LayerMask playerLayer = 0;
    [SerializeField] bool playerDetected = false;

    void Update()
    {
        //if (!target)
        //{
        //    enemyOwner.CanStartMoving = false;
        //}
        DetectEnemiesInRange();
        if(target)
            DetectTargetNoMoreInRange(target);
    }
    void Start()
    {
        Init();
    }

    void Init()
    {
        InitEvents();
        //player = FindObjectOfType<Player>();
        enemyOwner = GetComponent<Enemy>();
    }

    void InitEvents()
    {
        OnAggro += ManageDetect;
    }

    void ManageDetect(GameObject _target)
    {
        if (!enemyOwner || !_target)
        { 
        Debug.Log("failed to find owner or player");
        
        return;
        }
        enemyOwner.SetTarget(_target);
        enemyOwner.CanStartMoving = true;
    }

    void DetectEnemiesInRange()
    {
        centerRay = new Ray(rayCenterStart.transform.position, rayCenterStart.transform.forward * detectionRange);
        leftRay = new Ray(rayLeftStart.transform.position, rayLeftStart.transform.forward * detectionRange);
        rightRay = new Ray(rayRightStart.transform.position, rayRightStart.transform.forward * detectionRange);
        bool _hitCenter = Physics.Raycast(centerRay, out RaycastHit _hitPlayerCenter, detectionRange,playerLayer);
        bool _hitLeft = Physics.Raycast(leftRay, out RaycastHit _hitPlayerLeft, detectionRange,playerLayer);
        bool _hitRight = Physics.Raycast(rightRay, out RaycastHit _hitPlayerRight, detectionRange,playerLayer);

        if (_hitCenter)
            hitPlayer = _hitPlayerCenter;
        if (_hitLeft)
            hitPlayer = _hitPlayerLeft;
        if (_hitRight)
            hitPlayer = _hitPlayerRight;
       

        playerDetected = _hitCenter || _hitLeft || _hitRight;
        if (playerDetected)
        {
            
            Debug.DrawRay(centerRay.origin, centerRay.direction * detectionRange, Color.green);
            Debug.DrawRay(rightRay.origin, rightRay.direction * detectionRange, Color.green);
            Debug.DrawRay(leftRay.origin, leftRay.direction * detectionRange, Color.green);
            GameObject _target = hitPlayer.transform.gameObject;
            target = _target;
            OnAggro?.Invoke(_target);
            Debug.Log($"Player hit with detection sight : {_target}");
        }
        if(!playerDetected)
        {
            Debug.DrawRay(centerRay.origin, centerRay.direction * detectionRange, Color.red);
            Debug.DrawRay(rightRay.origin, rightRay.direction * detectionRange, Color.red);
            Debug.DrawRay(leftRay.origin, leftRay.direction * detectionRange, Color.red);

        }
        //Sphere detection
        //float _distance = Vector3.Distance(transform.position, allEnemiesToAggro[0].transform.position);
        //if (_distance <= detectionRange)
        //{
        //    Enemy _enemyToAggro = allEnemiesToAggro.OrderBy(c => Vector3.Distance(c.transform.position, transform.position)).FirstOrDefault();   // Lambda to order list () equivalent
        //    if (!_enemyToAggro) return;
        //    AggroEnemies(_enemyToAggro);
        //    OnAggro?.Invoke(_enemyToAggro);
        //}

    }

    void DetectTargetNoMoreInRange(GameObject _currentTarget)
    {
        float _distance = Vector3.Distance(transform.position, _currentTarget.transform.position);
        if (_distance >= detectionRange)
        {
            enemyOwner.SetTarget(null);
            enemyOwner.CanStartMoving = false;
            target = null;
            Debug.Log("Dropping aggro, target is out of range");   
        }
    }
    //GameObject GetClosest()
    //{
    //}


    private void OnDrawGizmos()
    {
        AnmaGizmos.DrawSphere(transform.position, detectionRange, Color.blue);

    }
}
