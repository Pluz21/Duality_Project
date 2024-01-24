using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class DetectionComponent : MonoBehaviour
{
    public event Action<Player> OnAggro = null;
    [SerializeField] Player player = null;

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

        DetectEnemiesInRange();
       // DetectEnemiesNoMoreInRange();
    }
    void Start()
    {
        Init();
    }

    void Init()
    {
        InitEvents();
        player = FindObjectOfType<Player>();
    }

    void InitEvents()
    {
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
        Debug.DrawRay(centerRay.origin, centerRay.direction * detectionRange);
        Debug.DrawRay(rightRay.origin, rightRay.direction * detectionRange);
        Debug.DrawRay(leftRay.origin, leftRay.direction * detectionRange);

        playerDetected = _hitCenter || _hitLeft || _hitRight;
        if (playerDetected)
        {
            
            Debug.DrawRay(centerRay.origin, centerRay.direction * detectionRange, Color.green);
            Debug.DrawRay(rightRay.origin, rightRay.direction * detectionRange, Color.green);
            Debug.DrawRay(leftRay.origin, leftRay.direction * detectionRange, Color.green);
            Debug.Log("Player hit with detection sight");
            OnAggro?.Invoke(hitPlayer.transform.GetComponent<Player>());
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

    void DetectEnemiesNoMoreInRange(Enemy _currentAggroedEnemy)
    {
        float _distance = Vector3.Distance(transform.position, _currentAggroedEnemy.transform.position);
        if (_distance >= detectionRange)
        {
            _currentAggroedEnemy.SetTarget(null);
        }
    }
    //GameObject GetClosest()
    //{
    //}

    void AggroEnemies(Enemy _enemyToAggro)
    {
        Debug.Log("In range to aggro");
        _enemyToAggro.SetTarget(gameObject.GetComponent<Player>());
        _enemyToAggro.CanStartMoving = true;
    }
    private void OnDrawGizmos()
    {
        AnmaGizmos.DrawSphere(transform.position, detectionRange, Color.blue);

    }
}
