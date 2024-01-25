using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class DetectionComponent : MonoBehaviour
{
    public event Action<Enemy> OnAggro = null;
    [SerializeField] float detectionRange = 10;
<<<<<<< Updated upstream
    [SerializeField] List<Enemy> allEnemiesToAggro = null; 
=======
    [SerializeField] int amountOfRays = 10;



>>>>>>> Stashed changes

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
        allEnemiesToAggro = FindObjectsByType<Enemy>(FindObjectsSortMode.None).ToList();
    }

    void InitEvents()
    {
    }
<<<<<<< Updated upstream
    void DetectEnemiesInRange()
    {
        //float _distance = Vector3.Distance(transform.position, allEnemiesToAggro[0].transform.position);
        //if (_distance <= detectionRange)
        //{
        //    Enemy _enemyToAggro = allEnemiesToAggro.OrderBy(c => Vector3.Distance(c.transform.position, transform.position)).FirstOrDefault();   // Lambda to order list () equivalent
        //    if (!_enemyToAggro) return;
        //    AggroEnemies(_enemyToAggro);
        //    OnAggro?.Invoke(_enemyToAggro);
        //}
       
=======


    void ManageDetect(GameObject _target)
    {
        if (!enemyOwner || !_target)
        { 
        enemyOwner.SetTarget(_target);

            return;
        }
        enemyOwner.PatrolComponent.CanPatrol = false;
        enemyOwner.SetTarget(_target);
        enemyOwner.CanStartMoving = true;
    }

    void DetectInRange()
    {
        List<Ray> _rays = new List<Ray>();

        for (int i = 0; i < amountOfRays; i++)
        {
            float _angle = -60 + i * 120 / amountOfRays;    // has to make 180 combined to align with the enemy
            Quaternion _rotation = Quaternion.Euler(0, _angle, 0);
            Vector3 direction = _rotation * rayCenterStart.transform.forward;

            Ray ray = new Ray(rayCenterStart.transform.position, direction * detectionRange);
            _rays.Add(ray);
        }

        int _size = _rays.Count;
  

        for (int i = 0; i < _size; i++)
        {
            bool _hit = Physics.Raycast(_rays[i], out RaycastHit _hitResult, detectionRange, playerLayer);

            if (_hit && _hitResult.transform.GetComponent<Player>())
            {
                hitPlayer = _hitResult;
                playerDetected = _hit;
                break;
            }

            if (playerDetected)
            {
                Debug.DrawRay(_rays[i].origin, _rays[i].direction * detectionRange, Color.green);

                GameObject _target = hitPlayer.transform.gameObject;
                if (!target)
                { 
                OnAggro?.Invoke(_target);
                Debug.Log($"Player hit with detection sight: {_target}");
                target = _target;
                }
            }
            else
            {
                Debug.DrawRay(_rays[i].origin, _rays[i].direction * detectionRange, Color.red);
                enemyOwner.SetTarget(null);
                OnAggro?.Invoke(null);
            }
        }







    }

    void DetectTargetNoMoreInRange(GameObject _currentTarget)
    {
        float _distance = Vector3.Distance(transform.position, _currentTarget.transform.position);
        if (_distance >= detectionRange)
        {
            enemyOwner.CanStartMoving = false;
            target = null;
            enemyOwner.SetTarget(null);
            playerDetected = false;
            OnAggroLoss?.Invoke(true);
            Debug.Log("Dropping aggro, target is out of range");   
        }
    }
    private void ManageAggroLoss(bool _value)
    {
        enemyOwner.PatrolComponent.CanPatrol = true;

>>>>>>> Stashed changes
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
