using UnityEngine;
using UnityEngine.Pool;

public class BombSpawner : BaseSpawner<Bomb>
{
    [SerializeField] private GenericPool<Bomb> _bombPool;

    public void RegisterCube(Cube cube)
    {
        cube.CubeDestroyed -= SpawnAtPosition;
        cube.CubeDestroyed += SpawnAtPosition;
    }

    private void SpawnAtPosition(Cube cube, Vector3 position)
    {
        Bomb bomb = _bombPool.Get();
        bomb.transform.SetPositionAndRotation(position, Quaternion.identity);
        bomb.Init(_bombPool);

        cube.CubeDestroyed -= SpawnAtPosition;
    }

    protected override void Spawn() { }
}
