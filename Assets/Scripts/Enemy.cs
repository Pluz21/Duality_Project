using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] bool canStartMoving = false;
    [SerializeField] Player target = null;
    [SerializeField] float enemyMoveSpeed = 6;

<<<<<<< Updated upstream
    public Player Target => target;
=======
    //Return to initial pos
    [SerializeField] Vector3 initialPos = Vector3.zero;
    [SerializeField] Quaternion initialRot = Quaternion.identity;
    [SerializeField] float minDistanceAllowedToInitialPos = 3;
    [SerializeField] float minDistanceAllowedToPlayer = 2;

    //Patrol
    [SerializeField] EnemyPatrolComponent patrolComponent = null;

    public EnemyPatrolComponent PatrolComponent => patrolComponent;

    public GameObject Target => target;
>>>>>>> Stashed changes
    public bool CanStartMoving 
    {
        get { return canStartMoving; }
        set { canStartMoving = value; }
    }
    void Start()
    {
<<<<<<< Updated upstream
        
=======
        Init();
    }

    void Init()
    {
        initialPos = transform.position;
        initialRot = transform.rotation;
        patrolComponent = GetComponent<EnemyPatrolComponent>();
>>>>>>> Stashed changes
    }

    void Update()
    {
<<<<<<< Updated upstream
        if (canStartMoving)
            MoveTo();
=======
        if (patrolComponent.CanPatrol) return;
        CheckDistanceToInitialPos();
        if (canStartMoving && target)
        {
            CheckDistanceToPlayer();
            MoveTo(target);
        }
        if (!target && !canStartMoving && transform.position != initialPos)
            canReturnToInitialPos = true;
        if (canReturnToInitialPos)
            MoveTo(initialPos);
>>>>>>> Stashed changes


    }
    public void SetTarget(Player _target)
    {
<<<<<<< Updated upstream
        if (!_target) return;
        target = _target;  
=======
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
>>>>>>> Stashed changes
    }

    public void MoveTo()
    {
        if (!target) return;
       transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * enemyMoveSpeed);
        
    }

    private void OnCollisionEnter(Collision _collision)
    {
        if (_collision == null) return;

    }
    private void OnDrawGizmos()
    {
        if(target)
        AnmaGizmos.DrawSphere(target.transform.position, 1, Color.green);
    }
}
