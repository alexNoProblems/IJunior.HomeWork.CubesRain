using System.Collections;
using UnityEngine;

public abstract class GenericSpawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] protected CubePositionRandomizer _randomazer;
    [SerializeField] protected float _spawnInterval;

    protected WaitForSeconds _waitForSecond;

    protected virtual bool UseTimer => true;

    protected virtual void Awake()
    {
        _waitForSecond = new WaitForSeconds(_spawnInterval);
    }

    protected virtual void Start()
    {
        if(UseTimer)
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

    protected abstract void Spawn();
}
