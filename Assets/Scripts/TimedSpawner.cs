using System.Collections;
using UnityEngine;

public abstract class TimedSpawner<T> : BaseSpawner<T> where T : MonoBehaviour
{
    [SerializeField] private float _spawnInterval = 0.5f;

    private WaitForSeconds _waitForSeconds;

    protected virtual void Awake()
    {
        _waitForSeconds = new WaitForSeconds(_spawnInterval);
    }

    protected virtual void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (enabled)
        {
            Spawn();

            yield return _waitForSeconds;
        }
    }
}
