using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{

    [SerializeField] List<Enemy> allEnemies = new List<Enemy>();

    public List<Enemy> AllEnemies => allEnemies;
    public int AllEnemiesCount => allEnemies.Count;

    

    public void AddElement(Enemy _toAdd)
    {
        if (!_toAdd || Exist(_toAdd)) return;
        allEnemies.Add(_toAdd);
        _toAdd.name += $"<{allEnemies.IndexOf(_toAdd)}> [MANAGED]";
    }

    public void RemoveElement(Enemy _toRemove)
    {
        if (!Exist(_toRemove)) return;
        allEnemies.Remove(_toRemove);
    }

    public void RemoveElement(int _index)
    {
        if (!Exist(_index)) return;
        allEnemies.RemoveAt(_index);
    }

    public void RemoveAll()
    {
        DestroyAllReferencedObjects();
        allEnemies.Clear();
    }

    public bool Exist(Enemy _toCheck)
    {
        return allEnemies.Contains(_toCheck);
    }

    public bool Exist(int _index)
    {
        if (_index >= allEnemies.Count || _index < 0) return false;
        return Exist(allEnemies[_index]);
    }

    void DestroyAllReferencedObjects()
    {
        foreach (Enemy _item in allEnemies)
        {
            Destroy(_item.gameObject);
        }
    }
}


