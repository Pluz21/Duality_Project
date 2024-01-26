using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;

public class EnemyPatrolComponent : MonoBehaviour
{
    public event Action<bool> OnPathReceived;
    [SerializeField] Path currentPath = null;
    [SerializeField] List<Vector3> allPoints = new List<Vector3>();
    [SerializeField] int pathIndex = 0;
    [SerializeField] bool canPatrol = true;
    [SerializeField] float patrolSpeed = 5;
    [SerializeField] float patrolRotateSpeed = 5;
    [SerializeField] float minDistanceAllowedtoPathPoint = 0.5f;

    [SerializeField] Enemy enemy = null;

    public bool CanPatrol
    {
        get { return canPatrol; }
        set { canPatrol = value; }
    }

    private void Awake()
    {
        OnPathReceived += (pathValid) =>
        {
            SetCanPatrol(pathValid);
            
            //($"Path Received. It is {(pathValid ? "Valid" : "invalid")}");
        };

    }
    void Start()
    {
        Init();
       
    }
    void Init()
    {
        enemy = GetComponent<Enemy>();
        SetPatrolPoints();
        OnPathReceived?.Invoke(true);
    }

    private void SetPatrolPoints()
    {
        allPoints = currentPath.FindPathPoints();
    }

    // Update is called once per frame
    void Update()
    {
        Patrol();
        RotateTowards();
    }

    private void Patrol()
    {
        if (!canPatrol)
        {
            return;
        }
        
        //transform.position = Vector3.MoveTowards(transform.position,
        //    _targetPos, Time.deltaTime * patrolSpeed);
        Vector3 _targetPos = currentPath.AllPathPoints[pathIndex];
        enemy.MoveTo(allPoints[pathIndex]);
        if (Vector3.Distance(transform.position, _targetPos) <= minDistanceAllowedtoPathPoint)
        {
            UpdatePathIndex();
        }

    }

    void RotateTowards()
    {
        if (!canPatrol) return;
        //Vector3 _lookAtDirection = currentPath.AllPathPoints[pathIndex] - transform.position;
        //if (_lookAtDirection == Vector3.zero) return;
        //Vector3 _newRot = Vector3.RotateTowards(transform.forward, _lookAtDirection, Time.deltaTime * patrolRotateSpeed,0);
        //transform.rotation = Quaternion.LookRotation(_newRot);
        enemy.RotateTo(currentPath.AllPathPoints[pathIndex]);
        
    }

    public void SetCanPatrol(bool _value)
    {
        canPatrol = _value;
    }

    void UpdatePathIndex()
    {
        if (pathIndex + 1 >= currentPath.AllPathPoints.Count)
        {
            pathIndex = 0;
            //SetCanPatrol(false);
            return;
        }
        pathIndex++;
    }
}
