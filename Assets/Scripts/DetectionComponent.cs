using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class DetectionComponent : MonoBehaviour
{
    public event Action<GameObject> OnAggro = null;
    public event Action<bool> OnAggroLoss = null;
    [SerializeField] GameObject target = null;
    [SerializeField] Enemy enemyOwner = null;

    // Raycast variables
    [SerializeField] RaycastHit hitPlayer;
    [SerializeField] GameObject rayCenterStart = null;
    [SerializeField] GameObject rayRightStart = null;
    [SerializeField] GameObject rayLeftStart = null;
    [SerializeField] Ray centerRay;
    [SerializeField] Ray rightRay;
    [SerializeField] Ray leftRay;
    [SerializeField] LayerMask playerLayer = 0;
    [SerializeField] bool playerDetected = false;
    [SerializeField] float detectionRange = 10;

    void Update()
    {

        DetectInRange();
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
        enemyOwner = GetComponent<Enemy>();
    }

    void InitEvents()
    {
        OnAggro += ManageDetect;
        OnAggroLoss += ManageAggroLoss;
    }


    void ManageDetect(GameObject _target)
    {
        if (!enemyOwner || !_target)
        { 
        Debug.Log("failed to find owner or player");
        enemyOwner.SetTarget(_target);

            return;
        }
        enemyOwner.SetTarget(_target);
        enemyOwner.CanStartMoving = true;
    }

    void DetectInRange()
    {
        List<Ray> _rays = new List<Ray>();
        centerRay = new Ray(rayCenterStart.transform.position, rayCenterStart.transform.forward * detectionRange);
        _rays.Add(centerRay);
        leftRay = new Ray(rayLeftStart.transform.position, rayLeftStart.transform.forward * detectionRange);
        _rays.Add(leftRay);
        rightRay = new Ray(rayRightStart.transform.position, rayRightStart.transform.forward * detectionRange);
        _rays.Add(rightRay);
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
            target = _target;
            OnAggro?.Invoke(_target);
            Debug.Log($"Player hit with detection sight: {_target}");
        }
        if(!playerDetected)
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
       
    }



    private void OnDrawGizmos()
    {
        AnmaGizmos.DrawSphere(transform.position, detectionRange, Color.blue);

    }

}
