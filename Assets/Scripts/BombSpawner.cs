using UnityEngine;
using UnityEngine.Pool;

public class BombSpawner : GenericSpawner<Bomb>
{
    [SerializeField] private GenericPool<Bomb> _bombPool;

    protected override bool UseTimer => false;

    public void RegisterCube(Cube cube)
    {
        cube.CubeDestroyed += SpawnAtPosition;
    }

    private void SpawnAtPosition(Vector3 position)
    {
        Bomb bomb = _bombPool.Get();
        bomb.transform.position = position;
        bomb.transform.rotation = Quaternion.identity;
        bomb.Init(_bombPool);
    }

    protected override void Spawn() { }
}
