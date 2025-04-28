using System.Collections;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private CubeFactory _factory;
    [SerializeField] private CubePositionRandomizer _positionRandomizer;
    [SerializeField] private float _spawnInterval = 0.5f;

    private Color _startColor;

    private void Start()
    {
        _startColor = new Color(Random.value, Random.value,Random.value);
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            SpawnCube();

            yield return new WaitForSeconds(_spawnInterval);
        }
    }

    private void SpawnCube()
    {
        Vector3 randomPosition = _positionRandomizer.GetRandomPositionAbovePlatform();
        Cube cube = _factory.GetCube();
        cube.transform.position = randomPosition;
        cube.transform.rotation = Quaternion.identity;
        cube.SetColor(_startColor);
            
        if (cube.TryGetComponent(out CubeLifetimeCount lifetimeCount))
            lifetimeCount.Initialize(_factory);
        else
            Debug.LogWarning($"Cube {cube.gameObject.name} не имеет компонента CubeLifetimeCount");
    }
}
