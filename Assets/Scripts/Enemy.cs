using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DetectionComponent))]
public class Enemy : MonoBehaviour
{
    [SerializeField] bool canStartMoving = false;
    [SerializeField] GameObject target = null;
    [SerializeField] float enemyMoveSpeed = 6;

    public GameObject Target => target;
    public bool CanStartMoving 
    {
        get { return canStartMoving; }
        set { canStartMoving = value; }
    }
    void Start()
    {
        
    }

    void Update()
    {
        if (canStartMoving && target)
            MoveTo();


    }
    public void SetTarget(GameObject _target)
    {
        if (!_target) return;
        target = _target;
        Debug.Log($"Target set! Now chasing : {_target}");
    }

    public void MoveTo()
    {
        if (!target || !canStartMoving) return;
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
