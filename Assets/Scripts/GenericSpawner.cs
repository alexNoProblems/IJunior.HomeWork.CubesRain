using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class GenericSpawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private ObjectPool<T> _pool;
    [SerializeField] private CubePositionRandomizer _randomozer;
    [SerializeField] private float _spawnInterval;

    private WaitForSeconds _waitForSecond;

    private void Awake()
    {
        _waitForSecond = new WaitForSeconds(_spawnInterval);
    }

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            Spawn();

            yield return _waitForSecond;
        }
    }

    protected virtual void Spawn(){}
}
