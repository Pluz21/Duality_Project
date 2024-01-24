using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Path : MonoBehaviour
{
    public event Action OnPathPointsCollected;
    [SerializeField] List<Vector3> allPathPoints = new List<Vector3>();
    
    public List<Vector3> AllPathPoints => allPathPoints;
    public Vector3 this[int _index] => allPathPoints[_index];

    private void Awake()
    {
        allPathPoints = FindPathPoints();
        Debug.Log("path called");
    }
    void Start()
    {

    }


    List<Vector3> FindPathPoints()
    {
        List<Vector3> _points = new List<Vector3>();
        foreach (Transform _child in transform)
        {
            Debug.Log(_child.name);
            _points.Add(_child.position);
            OnPathPointsCollected?.Invoke();

        }
        return _points;
    }


    private void OnDrawGizmos()
    {
        if (!Application.IsPlaying(this))
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(transform.GetChild(i).position, 0.2f);
                Gizmos.color = Color.white;
                if (i + 1 < transform.childCount)
                {
                    Gizmos.color = Color.blue;
                    Gizmos.DrawLine(transform.GetChild(i).position, transform.GetChild(i + 1).position);
                    Gizmos.color = Color.white;
                }
            }
            return;
        }
        if (allPathPoints.Count < 1) return;
        for (int i = 0; i < allPathPoints.Count; i++)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(allPathPoints[i], 0.2f);
            Gizmos.color = Color.white;
            if (i + 1 < allPathPoints.Count)
            {
                Gizmos.color = Color.magenta;
                Gizmos.DrawLine(allPathPoints[i], allPathPoints[i + 1]);
                Gizmos.color = Color.white;
            }
        }
    }
}
