using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class DetectionComponent : MonoBehaviour
{
    public event Action<Enemy> OnAggro = null;
    [SerializeField] float detectionRange = 10;
    [SerializeField] List<Enemy> allEnemiesToAggro = null; 

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
