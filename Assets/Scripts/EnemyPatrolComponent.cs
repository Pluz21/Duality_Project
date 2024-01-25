using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyPatrolComponent : MonoBehaviour
{
    public event Action<bool> OnPathReceived;
    [SerializeField] Path currentPath = null;
    [SerializeField] List<Vector3> allPoints = new List<Vector3>();
    [SerializeField] int pathIndex = 0;
    [SerializeField] int nextPathIndexValue = 1;
    [SerializeField] bool canPatrol = true;
    [SerializeField] float minDistanceAllowedtoPathPoint = 0.5f;

    //public Path CurrentPath
    //{
    //    get { return currentPath; }
    //    set
    //    {
    //        currentPath = value;
    //        bool _pathValid = currentPath.AllPathPoints.Count < 1 ? false : true;
    //        OnPathReceived?.Invoke(_pathValid);
    //        allPoints = currentPath.AllPathPoints;
    //    }
    //}
    private void Awake()
    {
        OnPathReceived += (pathValid) =>
        {
            SetCanPatrol(pathValid);
            Debug.Log($"Path Received... It is {(pathValid ? "Valid" : "invalid")}");
        };

    }
    // Start is called before the first frame update
    void Start()
    {
        currentPath.OnPathPointsCollected += SetPatrolPoints;
        
        // owner = GetComponent<SpawnEntity>();      Class version
    }

    private void SetPatrolPoints()
    {
        allPoints = currentPath.AllPathPoints;
        Debug.Log("patrol points set in the patrol component");
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
        Vector3 _targetPos = currentPath.AllPathPoints[pathIndex];
        transform.position = Vector3.MoveTowards(transform.position,
            _targetPos, Time.deltaTime * patrolSpeed);
        if (Vector3.Distance(transform.position, _targetPos) <= minDistanceAllowedtoPathPoint)
        {
            UpdatePathIndex();
        }

    }

    void RotateTowards()
    {
        if (!canPatrol) return;
        Vector3 _lookAtDirection = currentPath.AllPathPoints[pathIndex] - transform.position;
        if (_lookAtDirection == Vector3.zero) return;
        Quaternion _newRot = Quaternion.LookRotation(_lookAtDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, _newRot, Time.deltaTime * patrolRotateSpeed);
    }

    public void SetCanPatrol(bool _value)
    {
        canPatrol = _value;
    }

    void UpdatePathIndex()
    {
        if (pathIndex + nextPathIndexValue >= currentPath.AllPathPoints.Count)
        {
            SetCanPatrol(false);
            return;
        }
        pathIndex++;
    }
}
