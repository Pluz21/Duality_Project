
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(DetectionComponent))]
public class Enemy : MonoBehaviour
{
    //sound
    [SerializeField] AudioSource enemyPunchAttackSource = null;
    //[SerializeField] AudioClip attacksound = null;
    //anim
    public event Action OnAttack = null;
    //Events
    public event Action<bool> OnInRangeToPlayer;
    public event Action<bool> OnInRangeToAttackPlayer;
    public event Action OnTargetSet;

    //Manager
    [SerializeField] EnemyManager enemyManager = null;

    //Variables
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
    [SerializeField] float attackRangeOffsetToPlayer = 2;
    
    // Attack - Damage 
    [SerializeField] int damage = 1;
    [SerializeField] float attackSpeed = 0.5f;

    //Patrol
    [SerializeField] EnemyPatrolComponent patrolComponent = null;

    public EnemyPatrolComponent PatrolComponent => patrolComponent;

    public GameObject Target => target;
    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }
    public float AttackSpeed
    {
        get { return attackSpeed; }
        set { attackSpeed = value; }
    } 
    public float EnemyMoveSpeed
    {
        get { return enemyMoveSpeed; }
        set { enemyMoveSpeed = value; }
    }
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
        
        enemyManager = FindAnyObjectByType<EnemyManager>();
        enemyManager.AddElement(this);
        OnInRangeToPlayer += IsInRangeToPlayerLogic;
        OnInRangeToAttackPlayer += IsInRangeToAttackPlayerLogic;
        InvokeRepeating(nameof(AttackPlayer), 0, attackSpeed);

        initialPos = transform.position;
        initialRot = transform.rotation;
        patrolComponent = GetComponent<EnemyPatrolComponent>();

        SetLayersToIgnorePlayerRB();

        
      
        //DealDamage += animRef.updateAttackAnimator;
    }


    void Update()
    {
        EnemyLogic();

    }

    void SetLayersToIgnorePlayerRB()
    {
        gameObject.layer = 7;
        List<Transform> _allChildsObjects = GetComponentsInChildren<Transform>().ToList();
        int _size = _allChildsObjects.Count;
        for (int i = 0; i < _size; i++)
        {
            _allChildsObjects[i].gameObject.layer = 7;

        }
    }

    void EnemyLogic()
    {

        if (patrolComponent.CanPatrol) return;
        if (CheckPlayerIsInvisible())
        {
            DropAggroLogic();
            return;
        }
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

    public void DropAggroLogic()
    {
        canStartMoving = false;
        canReturnToInitialPos = true;
        isInRangeToPlayer = false;
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
            Debug.Log($"Target set! Now chasing : {_target}");
        }
    }

    public void MoveTo(GameObject _target)
    {
        transform.position = Vector3.MoveTowards(transform.position, _target.transform.position + new Vector3(0, 1, 0), Time.deltaTime * enemyMoveSpeed);
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
        Vector3 _newRotation = Vector3.RotateTowards(transform.forward, _targetDirection, Time.deltaTime * enemyRotateSpeed, 0);
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
        if (_distance <= minDistanceAllowedToPlayer + attackRangeOffsetToPlayer)
        { 
            Debug.Log("In Range TO ATTACK player");
            OnInRangeToAttackPlayer?.Invoke(true);
        if (_distance <= minDistanceAllowedToPlayer)    //&& !isInRangeToPlayer)
        {

            OnInRangeToPlayer?.Invoke(true);   // Do something here like dealing damage or moving backwards or whatever

        }
        }
        else if (_distance >= minDistanceAllowedToPlayer)
        {
            OnInRangeToPlayer?.Invoke(false);
            OnInRangeToAttackPlayer?.Invoke(false);
            Debug.Log("Melee Range of player");
        }


    }

    void IsInRangeToPlayerLogic(bool _value)
    {
        canStartMoving = !_value;
        //isInRangeToPlayer = _value;


    }
    void IsInRangeToAttackPlayerLogic(bool _value)
    {
        
        isInRangeToPlayer = _value;
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

    public void AttackPlayer()
    {
        if (isInRangeToPlayer && target)
        {
            DealDamage();
            OnAttack?.Invoke();
            enemyPunchAttackSource.Play();
            //enemyPunchAttackSource.Play();

        }
        else
            OnAttack?.Invoke();
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
            AnmaGizmos.DrawSphere(transform.position, minDistanceAllowedToPlayer + attackRangeOffsetToPlayer, Color.red);
            AnmaGizmos.DrawSphere(transform.position, minDistanceAllowedToPlayer, Color.magenta);
        //AnmaGizmos.DrawSphere(target.transform.position, 1, Color.green);
        //AnmaGizmos.DrawSphere(initialPos, 1, Color.magenta);
        //AnmaGizmos.DrawSphere(transform.position, minDistanceAllowedToPlayer, Color.red);
    }
}
