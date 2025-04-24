using UnityEngine;
using UnityEngine.Pool;

public class CubeFactory : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private int _poolCapacity = 10;
    [SerializeField] private int _maxPoolSize = 50;

    private ObjectPool<Cube> _cubesPool;

    private void Awake()
    {
        _cubesPool = new ObjectPool<Cube>(
        createFunc: () => Instantiate(_cubePrefab),
        actionOnGet: cube => cube.gameObject.SetActive(true),
        actionOnRelease: cube => cube.gameObject.SetActive(false),
        actionOnDestroy: cube => Destroy(cube),
        collectionCheck: true,
        defaultCapacity: _poolCapacity,
        maxSize: _maxPoolSize
        );
    }

    public Cube GetCube()
    {
        return _cubesPool.Get();
    }

    public void ReturnCube(Cube cube)
    {
        _cubesPool.Release(cube);
    }
}
