using System.Collections.Generic;
using UnityEngine;

public class GenericPool<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private T _prefab;
    [SerializeField] private int _poolCapacity;

    private Queue<T> _availableObjects = new Queue<T>();
    private int _createdCount;
    private int _spawnedEverCount;

    public int CreatedCount => _createdCount;
    public int SpawnedEverCount => _spawnedEverCount;
    public int AvailableObjectsCount => _availableObjects.Count;
    public int ActiveCount => _createdCount - _availableObjects.Count;

    private void Awake()
    {
        for (int i = 0; i < _poolCapacity; i++)
        {
            T obj = Instantiate(_prefab);
            obj.gameObject.SetActive(false);
            _availableObjects.Enqueue(obj);
            _createdCount++;
        }
    }

    public T Get()
    {
        T obj;

        if (_availableObjects.Count > 0)
        {
            obj = _availableObjects.Dequeue();
        }
        else
        {
            obj = Instantiate(_prefab);
            _createdCount++;
        }

        obj.gameObject.SetActive(true);
        _spawnedEverCount++;

        return obj;
    }

    public void Return(T obj)
    {
        obj.gameObject.SetActive(false);
        _availableObjects.Enqueue(obj);
    }
}
