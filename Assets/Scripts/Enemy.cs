using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(DetectionComponent))]
public class Enemy : MonoBehaviour
{
    [SerializeField] bool canStartMoving = false;
    [SerializeField] bool canReturnToInitialPos = false;

    [SerializeField] GameObject target = null;

    [SerializeField] float enemyMoveSpeed = 6;
    [SerializeField] float enemyRotateSpeed = 60;

    //Return to initial pos
    [SerializeField] Vector3 initialPos = Vector3.zero;
    [SerializeField] Quaternion initialRot = Quaternion.identity;
    [SerializeField] float minDistanceAllowed = 3;

    public GameObject Target => target;
    public bool CanStartMoving 
    {
        get { return canStartMoving; }
        set { canStartMoving = value; }
    }
    public bool CanReturnToInitialPos
    {
        get { return canReturnToInitialPos; }
        set { canReturnToInitialPos = value; }
    }

    void Start()
    {
        Init();
    }

    void Init()
    {
        initialPos = transform.position;
        initialRot = transform.rotation;
    }

    void Update()
    {
        CheckDistanceToInitialPos();
        if (canStartMoving && target)
            MoveTo(target);
        if (!target && !canStartMoving && transform.position != initialPos)
            canReturnToInitialPos = true;
        if (canReturnToInitialPos)
            MoveTo(initialPos);


    }
    public void SetTarget(GameObject _target)
    {
        if (!_target)
        {
            target = null;              // need to reset if target is null
            return;
        }
        target = _target;
        Debug.Log($"Target set! Now chasing : {_target}");
    }

    public void MoveTo(GameObject _target)
    {
       transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, Time.deltaTime * enemyMoveSpeed);
       RotateTo(_target.transform.position);

    }
    public void MoveTo(Vector3 _target)
    {
      
       transform.position = Vector3.MoveTowards(transform.position, _target, Time.deltaTime * enemyMoveSpeed);
       RotateTo(initialPos);
        

    }

    public void RotateTo(Vector3 _target)
    {
        Vector3 _targetDirection = (_target - transform.position);
        Vector3 _newRotation = Vector3.RotateTowards(transform.forward, _targetDirection, Time.deltaTime * enemyRotateSpeed,0);
        transform.rotation = Quaternion.LookRotation(_newRotation);
    }

    void CheckDistanceToInitialPos()
    {
        float _distance = Vector3.Distance(transform.position, initialPos);
        if (_distance <= minDistanceAllowed)
        {
            canReturnToInitialPos = false;
            transform.rotation = initialRot;

        }
    }
    private void OnCollisionEnter(Collision _collision)
    {
        if (_collision == null) return;

    }
    private void OnDrawGizmos()
    {
        if(target)
        AnmaGizmos.DrawSphere(target.transform.position, 1, Color.green);
        AnmaGizmos.DrawSphere(initialPos, 1, Color.magenta);
    }
}
