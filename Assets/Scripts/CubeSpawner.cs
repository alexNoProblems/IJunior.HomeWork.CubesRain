using UnityEngine.Pool;
using UnityEngine;

public class CubeSpawner : TimedSpawner<Cube>
{
    [SerializeField] private GenericPool<Cube> _cubePool;
    [SerializeField] private BombSpawner _bombSpawner;
    [SerializeField] private CubePositionRandomizer _randomizer;

    private Color _startColor;

    protected override void Start()
    {
        _startColor = new Color(Random.value, Random.value, Random.value);
        base.Start();
    }

    protected override void Spawn()
    {
        Vector3 randomPosition = _randomizer.GetRandomPositionAbovePlatform();
        Cube cube = _cubePool.Get();
        cube.transform.SetPositionAndRotation(randomPosition, Quaternion.identity);
        cube.Initialize(_cubePool, _startColor);

        _bombSpawner.RegisterCube(cube);
    }    
}
