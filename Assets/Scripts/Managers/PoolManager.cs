using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : ManagerBase<PoolManager> {
   


     [System.Serializable]
   public class PoolObjectGroup
    {
        public PoolObject poolObject;
        public int reqiredCount;
    }
    
    [SerializeField]private PoolObjectGroup[] _poolObjects;

    private Queue<PoolObject>[] _poolQueues;
    // Use this for initialization
    private new void Awake ()
    {
        base.Awake();
        _poolQueues = new Queue<PoolObject>[_poolObjects.Length];
        for (int i = 0; i < _poolQueues.Length; i++)
        {
            _poolQueues[i] = new Queue<PoolObject>();
            for (int j = 0; j < _poolObjects[i].reqiredCount; j++)
            {
                var _newPoolObject = Instantiate(_poolObjects[i].poolObject, transform);
                _newPoolObject.gameObject.SetActive(false);
                _poolQueues[i].Enqueue(_newPoolObject);

            }
        }
    }

    public MonoBehaviour getPoolObjectByName(string _name)
    {
        for (int i = 0; i < _poolObjects.Length; i++)
        {
            if (_poolObjects[i].poolObject.poolObjectName == _name)
            {
                return getPoolObjectByIndex(i);
            }
        }
        return null;
    }
    public MonoBehaviour getRandomPoolObjectByType(PoolObjectType _poolObjectType)
    {
        
        List<int> _indexes = new List<int>();
        for (int i = 0; i < _poolObjects.Length; i++)
        {
            if (_poolObjects[i].poolObject.poolObjectType == _poolObjectType)
            {
                _indexes.Add(i);             
            }
        }

        int _finalIndex = _indexes[Random.Range(0, _indexes.Count)];

        return getPoolObjectByIndex(_finalIndex);
    }

    public MonoBehaviour getPoolObjectByIndex(int _index)
    {
        if (_poolQueues[_index].Count > 0)
        {
            var _poolObject = _poolQueues[_index].Dequeue();
            _poolObject.gameObject.SetActive(true);
            return _poolObject.pooledBehaviour;
        }
        else
        {
           // Debug.Log("EMPTY " + _index);
            return Instantiate(_poolObjects[_index].poolObject, transform).pooledBehaviour;
        }
    }

    public void setPoolObjectBack(PoolObject _poolObject)
    {
        _poolObject.transform.SetParent(transform);
        _poolObject.gameObject.SetActive(false);
        _poolQueues[_poolObject.poolObjectIndex].Enqueue(_poolObject);

     }

}
