using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;
using System;

[RequireComponent(typeof(DetectionComponent))]
public class Enemy : MonoBehaviour
{
    public event Action<bool> OnInRangeToPlayer;
    public event Action OnTargetSet;
    [SerializeField] bool canStartMoving = false;
    [SerializeField] bool canReturnToInitialPos = false;
    [SerializeField] bool isInRangeToPlayer = false;

    [SerializeField] GameObject target = null;

    [SerializeField] float enemyMoveSpeed = 6;
    [SerializeField] float enemyRotateSpeed = 60;

    //Return to initial pos
    [SerializeField] Vector3 initialPos = Vector3.zero;
    [SerializeField] Quaternion initialRot = Quaternion.identity;
    [SerializeField] float minDistanceAllowedToInitialPos = 3;
    [SerializeField] float minDistanceAllowedToPlayer = 2;

    // Attack - Damage 
    [SerializeField] int damage = 1;
    [SerializeField] float attackSpeed = 0.5f;

    //Patrol
    [SerializeField] EnemyPatrolComponent patrolComponent = null;

    public EnemyPatrolComponent PatrolComponent => patrolComponent;

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
        OnInRangeToPlayer += IsInRangeToPlayerLogic;
        InvokeRepeating(nameof(AttackPlayer), 0, attackSpeed);

        initialPos = transform.position;
        initialRot = transform.rotation;
        patrolComponent = GetComponent<EnemyPatrolComponent>();
    }

    public void DropAggroLogic()
    {
        canStartMoving = false;
        canReturnToInitialPos = true;
        isInRangeToPlayer = false;
    }


    void Update()
    {
        EnemyLogic();

    }

    void EnemyLogic()
    {
        if (patrolComponent.CanPatrol) return;
        CheckDistanceToInitialPos();

        CheckDistanceToPlayer();
       
            
        if (canStartMoving && target)
        {
            MoveTo(target);
        }
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
        if (!target)
        { 
        OnTargetSet?.Invoke();
        target = _target;
        //Debug.Log($"Target set! Now chasing : {_target}");
        }
    }

    public void MoveTo(GameObject _target)
    {
       transform.position = Vector3.MoveTowards(transform.position, _target.transform.position + new Vector3(0,1,0), Time.deltaTime * enemyMoveSpeed);
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
        if (_distance <= minDistanceAllowedToInitialPos)
        {
            canReturnToInitialPos = false;
            transform.rotation = initialRot;

        }
    }
    void CheckDistanceToPlayer()
    {
        float _distance = Vector3.Distance(transform.position, target.transform.position);
        if (_distance <= minDistanceAllowedToPlayer && !isInRangeToPlayer)
        {
            //Debug.Log("In Melee Range of player");

            OnInRangeToPlayer?.Invoke(true);   // Do something here like dealing damage or moving backwards or whatever

        }
        else if (_distance >= minDistanceAllowedToPlayer)
        { 
            OnInRangeToPlayer?.Invoke(false);
            //Debug.Log("Melee Range of player");
        }
     

    }



    void IsInRangeToPlayerLogic(bool _value)
    {
        canStartMoving = !_value;
        isInRangeToPlayer = _value;


    }


    void AttackPlayer()
    {
        if (isInRangeToPlayer && target)
        {
            DealDamage();
        }
    }

   public bool CheckPlayerIsInvisible()
    {
        if (target && target.GetComponent<Player>())
        {
            Player _target = target.GetComponent<Player>();
            bool _isInvi = _target.GetComponent<MovementComponent>().IsInvisible;
            if (_isInvi)
                return true;

        }
        return false;
    }
    void DealDamage()
    {
        if (!target)return;       
        Player _player = target.GetComponent<Player>();
        _player.Life -= damage;
    }
    private void OnCollisionEnter(Collision _collision)
    {
        if (_collision == null) return;

    }
    private void OnDrawGizmos()
    {
        //if(target)
        //AnmaGizmos.DrawSphere(target.transform.position, 1, Color.green);
        //AnmaGizmos.DrawSphere(initialPos, 1, Color.magenta);
        //AnmaGizmos.DrawSphere(transform.position, minDistanceAllowedToPlayer, Color.red);
    }
}
